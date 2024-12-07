namespace ToyPlCorrectness.Application;

public abstract record Comparator(Func<UnsignedIntModType, UnsignedIntModType, bool> Predicate)
{
    public static Comparator FromString(string line)
    {
        return line switch
        {
            "=" => Equal.Create,
            "/=" => NotEqual.Create,
            ">" => Greater.Create,
            ">=" => GreaterOrEqual.Create,
            "<" => Less.Create,
            "<=" => LessOrEqual.Create,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}
public record LessOrEqual() : Comparator((l, r) => l.Value <= r.Value)
{
    public static Comparator Create => new LessOrEqual();

    public override string ToString() => "<=";
}

public record NotEqual() : Comparator((l, r) => l.Value != r.Value)
{
    public static Comparator Create => new NotEqual();

    public override string ToString() => "/=";
}

public record Less() : Comparator((l, r) => l.Value < r.Value)
{
    public static Comparator Create => new Less();

    public override string ToString() => "<";
}

public record GreaterOrEqual() : Comparator((l, r) => l.Value >= r.Value)
{
    public static Comparator Create => new GreaterOrEqual();


    public override string ToString() => ">=";
}

public record Greater() : Comparator((l, r) => l.Value > r.Value)
{
    public static Comparator Create => new Greater();

    public override string ToString() => ">";
}


public record Equal() : Comparator((l, r) => l.Value == r.Value)
{
    public static Comparator Create => new Equal();

    public override string ToString() => "=";
}