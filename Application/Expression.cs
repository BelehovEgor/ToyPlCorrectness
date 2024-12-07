namespace ToyPlCorrectness.Application;

public record Expression(
    PossibleValue Left,
    PossibleValue? Right,
    Operation Operation)
{
    public override string ToString()
    {
        return $"({Left.ToStr()}{Operation}{Right?.ToStr() ?? ""})";
    }
}

public abstract class Operation(string symbol)
{
    public static Operation FromString(string line)
    {
        return line switch
        {
            "+" => new PlusOperation(),
            "-" => new MinusOperation(),
            "*" => new TimesOperation(),
            "/" => new DivOperation(),
            "%" => new ModOperation(),
            "!" => new FactorialOperation(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public override string ToString() => symbol;
}

public class DivOperation() : Operation("/");

public class MinusOperation() : Operation("-");

public class ModOperation() : Operation("%");

public class PlusOperation() : Operation("+");

public class TimesOperation() : Operation("*");

public class FactorialOperation() : Operation("!");