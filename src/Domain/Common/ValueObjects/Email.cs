using System.ComponentModel.DataAnnotations;

namespace Domain.Common.ValueObjects;

public class Email : ValueObject
{
    static Email()
    {
    }

    private Email()
    {
    }

    private Email(string value)
    {
        Value = value.ToLower();
    }

    public string Value { get; } = null!;

    public static Email From(string @string)
    {
        if (!IsValid(@string))
        {
            throw CustomException.WithBadRequest($"Invalid email id: {@string}");
        }

        var @object = new Email(@string);
        return @object;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    private static bool IsValid(string value)
    {
        if (!new EmailAddressAttribute().IsValid(value))
        {
            return false;
        }

        return value.Contains('.') && !value.Contains("..") && value.Split("@").Length == 2;
    }
}
