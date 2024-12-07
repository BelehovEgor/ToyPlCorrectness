using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using ToyPlCorrectness.Application;

namespace ToyPlCorrectness.Language;

public static class ToyPlTranslator
{
    public static (ProgramWithCondition, HashSet<string> variables) GetProgram(string fileName)
    {
        using StreamReader reader = new(fileName);

        var code = reader.ReadToEnd();

        var stream = CharStreams.fromString(code);
        var lexer = new toyPlLexer(stream);
        var tokens = new CommonTokenStream(lexer);
        var parser = new toyPlParser(tokens);

        var vars = new HashSet<string>();
        return (VisitProgram(parser.program(), vars), vars);
    }

    public static PossibleValue GetPossibleValue(string code, HashSet<string> vars)
    {
        if (code.All(char.IsLetter))
        {
            vars.Add(code);
            return code;
        }

        if (code.All(char.IsDigit))
        {
            return new UnsignedIntModType(uint.Parse(code));
        }

        var stream = CharStreams.fromString(code);
        var lexer = new toyPlLexer(stream);
        var tokens = new CommonTokenStream(lexer);
        var parser = new toyPlParser(tokens);

        return GetExpression(parser.expr(), vars);
    }

    private static ProgramWithCondition VisitProgram(toyPlParser.ProgramContext context, HashSet<string> vars)
    {
        return context.children switch
        {
            [
                TerminalNodeImpl { Symbol.Text: "{" },
                toyPlParser.CondContext pred,
                TerminalNodeImpl { Symbol.Text: "}" },
                toyPlParser.StatementContext statement,
                TerminalNodeImpl { Symbol.Text: "{" },
                toyPlParser.CondContext post,
                TerminalNodeImpl { Symbol.Text: "}" },
            ] => new ProgramWithCondition(GetPredicate(pred, vars), VisitStatement(statement, vars), GetPredicate(post, vars)),

            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private static IOperation VisitStatement(toyPlParser.StatementContext context, HashSet<string> vars)
    {
        return context.children switch
        {
            [
                toyPlParser.VarContext varContext,
                TerminalNodeImpl { Symbol.Text: ":=" },
                toyPlParser.ExprContext exprContext
            ] => new AssignOperation(GetVar(varContext, vars), GetExpression(exprContext, vars)),

            [
                TerminalNodeImpl { Symbol.Text: "(" },
                toyPlParser.StatementContext left,
                TerminalNodeImpl { Symbol.Text: ";" },
                toyPlParser.StatementContext right,
                TerminalNodeImpl { Symbol.Text: ")" },
            ] => new CompositionOperation(VisitStatement(left, vars), VisitStatement(right, vars)),

            [
                TerminalNodeImpl { Symbol.Text: "(" },
                TerminalNodeImpl { Symbol.Text: "if" },
                toyPlParser.CondContext pred,
                TerminalNodeImpl { Symbol.Text: "then" },
                toyPlParser.StatementContext then,
                TerminalNodeImpl { Symbol.Text: "else" },
                toyPlParser.StatementContext @else,
                TerminalNodeImpl { Symbol.Text: ")" }
            ] => new IfOperation(GetPredicate(pred, vars), VisitStatement(then, vars), VisitStatement(@else, vars)),

            [
                TerminalNodeImpl { Symbol.Text: "(" },
                TerminalNodeImpl { Symbol.Text: "{" },
                toyPlParser.CondContext invariant,
                TerminalNodeImpl { Symbol.Text: "}" },
                TerminalNodeImpl { Symbol.Text: "while" },
                toyPlParser.CondContext pred,
                TerminalNodeImpl { Symbol.Text: "do" },
                toyPlParser.StatementContext @do,
                TerminalNodeImpl { Symbol.Text: ")" }
            ] => new WhileOperation(GetPredicate(invariant, vars), GetPredicate(pred, vars), VisitStatement(@do, vars)),

            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private static string GetVar(toyPlParser.VarContext varContext, HashSet<string> vars)
    {
        var name = varContext.GetText();

        vars.Add(name);

        return name;
    }

    private static PossibleValue GetExpression(toyPlParser.ExprContext exprContext, HashSet<string> vars)
    {
        return exprContext.children switch
        {
            [toyPlParser.VarContext varContext] => GetVar(varContext, vars),

            [TerminalNodeImpl terminalNodeImpl] => new UnsignedIntModType(uint.Parse(terminalNodeImpl.GetText())),
            
            [
                TerminalNodeImpl { Symbol.Text: "(" },
                toyPlParser.ExprContext left,
                toyPlParser.Int_opContext opContext,
                toyPlParser.ExprContext right,
                TerminalNodeImpl { Symbol.Text: ")" }
            ] => new Expression(
                    GetExpression(left, vars),
                    GetExpression(right, vars),
                    GetOperation(opContext)),

            [
                TerminalNodeImpl { Symbol.Text: "(" },
                toyPlParser.ExprContext left,
                TerminalNodeImpl { Symbol.Text: "!" },
                TerminalNodeImpl { Symbol.Text: ")" }
            ] => new Expression(
                    GetExpression(left, vars),
                    null,
                    new FactorialOperation()),

            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private static ICondition GetPredicate(toyPlParser.CondContext condContext, HashSet<string> vars)
    {
        return condContext.children switch
        {
        [
            TerminalNodeImpl { Symbol.Text: "(" },
            toyPlParser.ExprContext left,
            toyPlParser.Cond_int_opContext opContext,
            toyPlParser.ExprContext right,
            TerminalNodeImpl { Symbol.Text: ")" }
        ] => new Condition(GetExpression(left, vars), GetComparator(opContext), GetExpression(right, vars)),

        [
            TerminalNodeImpl { Symbol.Text: "(" },
            TerminalNodeImpl { Symbol.Text: "!" },
            toyPlParser.CondContext cond,
            TerminalNodeImpl { Symbol.Text: ")" }
        ] => new NotCondition(GetPredicate(cond, vars)),

        [
            TerminalNodeImpl { Symbol.Text: "(" },
            toyPlParser.CondContext left,
            toyPlParser.Cond_bool_opContext opContext,
            toyPlParser.CondContext right,
            TerminalNodeImpl { Symbol.Text: ")" }
        ] when opContext.GetText() == "&&"
                => new AndCondition(GetPredicate(left, vars), GetPredicate(right, vars)),

                [
                    TerminalNodeImpl { Symbol.Text: "(" },
                    toyPlParser.CondContext left,
                    toyPlParser.Cond_bool_opContext opContext,
                    toyPlParser.CondContext right,
                    TerminalNodeImpl { Symbol.Text: ")" }
                ] when opContext.GetText() == "||"
                    => new OrCondition(GetPredicate(left, vars), GetPredicate(right, vars)),

                    [
                    TerminalNodeImpl { Symbol.Text: "(" },
                    toyPlParser.CondContext cond,
                    TerminalNodeImpl { Symbol.Text: ")" }
                ] => GetPredicate(cond, vars),

            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private static Comparator GetComparator(toyPlParser.Cond_int_opContext opContext)
    {
        return Comparator.FromString(opContext.GetText());
    }

    private static Operation GetOperation(toyPlParser.Int_opContext opContext)
    {
        return Operation.FromString(opContext.GetText());
    }
}