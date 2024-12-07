namespace ToyPlCorrectness.Application;

public interface ICondition
{
    ICondition Substitute(string varName, PossibleValue value);
}

public abstract record ConditionBase : ICondition
{
    public abstract ICondition Substitute(string varName, PossibleValue value);
}

public record Condition(PossibleValue Left, Comparator Comparator, PossibleValue Right) : ConditionBase
{
    public override string ToString()
    {
        return $"({Left.ToStr()} {Comparator} {Right.ToStr()})";
    }

    public override ICondition Substitute(string varName, PossibleValue value)
    {
        return new Condition(Left.Substitute(varName, value), Comparator, Right.Substitute(varName, value));
    }
}

public record NotCondition(ICondition Condition) : ConditionBase
{
    public override string ToString()
    {
        return $"! {Condition}";
    }

    public override ICondition Substitute(string varName, PossibleValue value)
    {
        return new NotCondition(Condition.Substitute(varName, value));
    }
}

public record AndCondition(ICondition Left, ICondition Right) : ConditionBase
{
    public override string ToString()
    {
        return $"({Left} && {Right})";
    }

    public override ICondition Substitute(string varName, PossibleValue value)
    {
        return new AndCondition(Left.Substitute(varName, value), Right.Substitute(varName, value));
    }
}

public record OrCondition(ICondition Left, ICondition Right) : ConditionBase
{
    public override string ToString()
    {
        return $"({Left} || {Right})";
    }

    public override ICondition Substitute(string varName, PossibleValue value)
    {
        return new OrCondition(Left.Substitute(varName, value), Right.Substitute(varName, value));
    }
}

public record ImplicationCondition(ICondition Left, ICondition Right) : ConditionBase
{
    public override string ToString()
    {
        return $"({Left} -> {Right})";
    }

    public override ICondition Substitute(string varName, PossibleValue value)
    {
        return new OrCondition(Left.Substitute(varName, value), Right.Substitute(varName, value));
    }
}
