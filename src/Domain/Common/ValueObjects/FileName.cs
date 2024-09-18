namespace Domain.Common.ValueObjects;

public class FileName : ValueObject
{
    static FileName()
    {
    }

    private FileName()
    {
    }

    private FileName(string value)
    {
        Value = value;
    }

    public string Value { get; } = null!;

    public static FileName From(string fileName)
    {
        if (!IsValid(fileName))
        {
            throw CustomException.WithBadRequest($"Invalid file name: {fileName}");
        }

        if (fileName.Split(".").Length != 2)
        {
            throw CustomException.WithBadRequest($"Invalid file name: {fileName}");
        }

        var @object = new FileName(fileName);
        return @object;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    private static bool IsValid(string value)
    {
        return !string.IsNullOrEmpty(value);
    }
}
