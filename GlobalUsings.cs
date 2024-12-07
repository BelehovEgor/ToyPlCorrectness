global using PossibleValue = OneOf.OneOf<string, ToyPlCorrectness.Application.UnsignedIntModType, ToyPlCorrectness.Application.Expression>;
using ToyPlCorrectness.Application;

public static class Extensions
{
    public static string ToStr(this PossibleValue value)
    {
        return value.Value switch
        {
            string name => name,
            UnsignedIntModType intModTypeValue => intModTypeValue.ToString(),
            Expression expression => expression.ToString(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public static PossibleValue Substitute(this PossibleValue current, string name, PossibleValue value)
    {
        return current.Value switch
        {
            string varName => varName == name ? value : current,
            UnsignedIntModType => current,
            Expression expression => new Expression(
                expression.Left.Substitute(name, value),
                expression.Right?.Substitute(name, value),
                expression.Operation),
            _ => throw new NotImplementedException(),
        };
    }
}

