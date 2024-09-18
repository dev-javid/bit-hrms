using System.Text.RegularExpressions;

namespace Domain.Common.ValueObjects;

#pragma warning disable S101 // Types should be named in PascalCase
public class PAN : ValueObject
#pragma warning restore S101 // Types should be named in PascalCase
{
    static PAN()
    {
    }

    private PAN()
    {
    }

    private PAN(string value)
    {
        Value = value;
    }

    public string Value { get; } = null!;

    public static PAN From(string @string)
    {
        if (!IsValid(@string))
        {
            throw CustomException.WithBadRequest($"Invalid PAN: {@string}");
        }

        var @object = new PAN(@string);
        return @object;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    private static bool IsValid(string value)
    {
        return value.Length == 10 && Regex.IsMatch(value, "[A-Z]{5}[0-9]{4}[A-Z]{1}");
    }
}
