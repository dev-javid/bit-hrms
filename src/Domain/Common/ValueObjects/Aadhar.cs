using System.Text.RegularExpressions;

namespace Domain.Common.ValueObjects;

public class Aadhar : ValueObject
{
    static Aadhar()
    {
    }

    private Aadhar()
    {
    }

    private Aadhar(string value)
    {
        Value = value;
    }

    public string Value { get; } = null!;

    public static Aadhar From(string @string)
    {
        if (!IsValid(@string))
        {
            throw CustomException.WithBadRequest($"Invalid Aadhar: {@string}");
        }

        var @object = new Aadhar(@string);
        return @object;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    private static bool IsValid(string value)
    {
        return Regex.IsMatch(value, "^\\d{4}\\d{4}\\d{4}$");
    }
}
