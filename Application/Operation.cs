using System.Reflection.Metadata.Ecma335;

namespace ToyPlCorrectness.Application;

public interface IOperation
{
    ICondition GetAc(ICondition post);
    ICondition[] GetVc(ICondition post);
}

public record ProgramWithCondition(ICondition Pred, IOperation Prog, ICondition Post)
{
    public override string ToString()
    {
        return $"{{{Pred}}} {Prog} {{{Post}}}";
    }

    public ICondition[] GetVc()
    {
        var progVc = Prog.GetVc(Post);
        var last = new ImplicationCondition(Pred, Prog.GetAc(Post));

        return progVc.Concat([last]).ToArray();
    }
}

public record AssignOperation(string LeftValue, PossibleValue RightValue) : IOperation
{
    public ICondition GetAc(ICondition post)
    {
        return post.Substitute(LeftValue, RightValue);
    }

    public ICondition[] GetVc(ICondition post)
    {
        return [];
    }

    public override string ToString()
    {
        return $"{LeftValue} := {RightValue.ToStr()}";
    }
}

public record CompositionOperation(IOperation First, IOperation Second) : IOperation
{
    public ICondition GetAc(ICondition post)
    {
        var secondPre = Second.GetAc(post);
        var pre = First.GetAc(secondPre);

        return pre;
    }

    public ICondition[] GetVc(ICondition post)
    {
        var vcB = Second.GetVc(post);
        var vcA = First.GetVc(Second.GetAc(post));

        return vcA.Union(vcB).ToArray();
    }

    public override string ToString()
    {
        return $"({First} ; {Second})";
    }
}

public record IfOperation(ICondition Condition, IOperation Then, IOperation Else) : IOperation
{
    public ICondition GetAc(ICondition post)
    {
        var thenPred = new AndCondition(Condition, Then.GetAc(post));
        var elsePred = new AndCondition(new NotCondition(Condition), Else.GetAc(post));

        return new OrCondition(thenPred, elsePred);
    }

    public ICondition[] GetVc(ICondition post)
    {
        var vcThen = Then.GetVc(post);
        var vcElse = Then.GetVc(post);

        return vcThen.Union(vcElse).ToArray();
    }

    public override string ToString()
    {
        return $" if {Condition} then {Then} else {Else}";
    }
}

public record WhileOperation(ICondition Invariant, ICondition Condition, IOperation Do) : IOperation
{
    public ICondition GetAc(ICondition post)
    {
        return Invariant;
    }

    public ICondition[] GetVc(ICondition post)
    {
        var vcA = Do.GetVc(post);
        var vcInvariant = new[] 
            { 
                new ImplicationCondition(
                    new AndCondition(
                        Invariant,
                        new NotCondition(Condition)),
                    post),
                new ImplicationCondition(
                    new AndCondition(
                        Invariant,
                        Condition),
                    Do.GetAc(Invariant))
            };

        return vcA.Union(vcInvariant).ToArray();
    }

    public override string ToString()
    {
        return $"{{{ Invariant }}} while {Condition} do {Do}";
    }
}
