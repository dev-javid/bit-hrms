namespace Domain.Common.ValueObjects
{
    public class FinancialMonth : ValueObject
    {
        static FinancialMonth()
        {
        }

        private FinancialMonth()
        {
        }

        private FinancialMonth(int value)
        {
            Value = value;
        }

        public int Value { get; }

        public static FinancialMonth From(int month)
        {
            if (!IsValid(month))
            {
                throw CustomException.WithBadRequest($"Invalid month: {month}");
            }

            return new FinancialMonth(month);
        }

        public DateTime FirstDay()
        {
            var today = DateTime.Today;
            if (Value == 1)
            {
                return new DateTime(today.Year, Value, 1, 0, 0, 0, 0, 0, DateTimeKind.Unspecified);
            }
            else
            {
                if (today.Month < 3)
                {
                    return new DateTime(today.Year, Value - 1, 1, 0, 0, 0, 0, 0, DateTimeKind.Unspecified);
                }

                return new DateTime(today.Year, Value - 1, 1, 0, 0, 0, 0, 0, DateTimeKind.Unspecified);
            }
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        private static bool IsValid(int month)
        {
            return month == 1 || month == 3;
        }
    }
}
