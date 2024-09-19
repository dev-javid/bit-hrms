namespace Application.Common.Validators
{
    public class Base64FileValidator : AbstractValidator<string?>
    {
        private const string Base64Separator = ";base64,";
        private readonly double _fileSizeLimitInMbs = 1;

        public Base64FileValidator()
        {
            RuleFor(x => x)
                .Must(BeValidBase64Format!)
                .WithMessage("The input must be a valid Base64 string");

            RuleFor(x => x)
                .Custom((value, context) =>
                {
                    if (!NotExceedSizeLimit(value!))
                    {
                        context.AddFailure(context.PropertyName,
                            $"The file size for '{context.PropertyName}' exceeds the maximum limit of {_fileSizeLimitInMbs} MB.");
                    }
                });
        }

        private static bool BeValidBase64Format(string input)
        {
            var base64Data = ExtractBase64Data(input);
            if (string.IsNullOrEmpty(base64Data))
            {
                return false;
            }

            try
            {
                Convert.FromBase64String(base64Data);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private static string ExtractBase64Data(string input)
        {
            int separatorIndex = input.IndexOf(Base64Separator, StringComparison.Ordinal);
            if (separatorIndex < 0 || separatorIndex + Base64Separator.Length >= input.Length)
            {
                return string.Empty;
            }

            return input[(separatorIndex + Base64Separator.Length)..];
        }

        private bool NotExceedSizeLimit(string input)
        {
            var base64Data = ExtractBase64Data(input);
            if (string.IsNullOrEmpty(base64Data))
            {
                return false;
            }

            var byteData = Convert.FromBase64String(base64Data);
            double fileSizeInMb = byteData.Length / (1024.0 * 1024.0);
            return fileSizeInMb <= _fileSizeLimitInMbs;
        }
    }
}
