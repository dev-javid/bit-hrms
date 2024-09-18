namespace Domain.Identity
{
    public class RefreshToken
    {
        public Guid Id { get; private set; }

        public int UserId { get; private set; }

        public string Value { get; private set; } = null!;

        public string DeviceIdentifier { get; private set; } = null!;

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public static RefreshToken Create(int userId, string value, string deviceIdentifier)
        {
            return new RefreshToken
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Value = value,
                DeviceIdentifier = deviceIdentifier,
                CreatedOn = DateTime.UtcNow,
            };
        }

        public void Update(string value)
        {
            Value = value;
            ModifiedOn = DateTime.UtcNow;
        }
    }
}
