namespace Domain.Common
{
    public static class Extensions
    {
        public static T ToValueObject<T>(this string @string) where T : ValueObject
        {
            return typeof(T).Name switch
            {
                nameof(Email) => (T)(object)Email.From(@string),
                nameof(PhoneNumber) => (T)(object)PhoneNumber.From(@string),
                nameof(FileName) => (T)(object)FileName.From(@string),
                nameof(PAN) => (T)(object)PAN.From(@string),
                nameof(Aadhar) => (T)(object)Aadhar.From(@string),
                _ => throw new ArgumentException($"Unsupported type: {typeof(T).Name}"),
            };
        }

        public static T ToValueObject<T>(this int @int) where T : ValueObject
        {
            return typeof(T).Name switch
            {
                nameof(FinancialMonth) => (T)(object)FinancialMonth.From(@int),
                _ => throw new ArgumentException($"Unsupported type: {typeof(T).Name}"),
            };
        }

        public static T AsEnum<T>(this string @string) where T : struct
        {
            return Enum.Parse<T>(@string, true);
        }

        public static DateOnly ToDateOnly(this DateTime dateTime)
        {
            return DateOnly.FromDateTime(dateTime);
        }

        public static TimeOnly ToTimeOnly(this DateTime dateTime)
        {
            return TimeOnly.FromDateTime(dateTime);
        }

        public static DateOnly FirstDayOfMonth(this DateOnly date)
        {
            return new DateOnly(date.Year, date.Month, 1);
        }

        public static DateOnly LastDayOfMonth(this DateOnly date)
        {
            return date.FirstDayOfMonth().AddMonths(1).AddDays(-1);
        }

        public static bool IsWeeklyOff(this DateTime dateTime)
        {
            if (dateTime.DayOfWeek == DayOfWeek.Sunday)
            {
                return true;
            }
            else if (dateTime.DayOfWeek == DayOfWeek.Saturday)
            {
                var refDate = new DateTime(2024, 4, 21, 0, 0, 0, 0, 0, DateTimeKind.Unspecified);
                var daysPassed = (dateTime - refDate).Days;
                return (daysPassed % 14) % 2 != 0;
            }

            return false;
        }

        public static bool IsWeeklyOff(this DateOnly dateTime)
        {
            return dateTime.ToDateTime(TimeOnly.MinValue).IsWeeklyOff();
        }

        public static byte[] ToBytes(this decimal value)
        {
            int[] bits = decimal.GetBits(value);
            byte[] bytes = new byte[16];

            Buffer.BlockCopy(bits, 0, bytes, 0, 16);

            return bytes;
        }

        public static decimal ToDecimal(this byte[] bytes)
        {
            if (bytes.Length != 16)
            {
                throw new ArgumentException("Byte array must be exactly 16 bytes long.");
            }

            int[] bits = new int[4];
            Buffer.BlockCopy(bytes, 0, bits, 0, 16);
            return new decimal(bits);
        }
    }
}
