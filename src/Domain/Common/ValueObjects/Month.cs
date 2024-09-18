namespace Domain.Common.ValueObjects
{
    public class Month : ValueObject
    {
        private Month(int value)
        {
            Value = value;
        }

        public int Value { get; }

        public static Month From(int month)
        {
            if (!IsValid(month))
            {
                throw CustomException.WithBadRequest($"Invalid month: {month}");
            }

            return new Month(month);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        private static bool IsValid(int value)
        {
            return value >= 1 && value <= 12;
        }
    }
}
