namespace Tests.Unit.Application
{
    internal static class Extensions
    {
        public static T WithId<T, TId>(this T entity, int value)
            where T : Entity<TId>
            where TId : struct
        {
            Type entityType = entity.GetType();
            var idProperty = entityType?.BaseType?.GetProperty("Id");
            idProperty?.SetValue(entity, value);
            return entity;
        }

        public static T SetProperty<T, TValue>(this T entity, string propertyName, TValue value)
            where T : class
        {
            Type entityType = entity.GetType();
            var property = entityType?.GetProperty(propertyName);
            property?.SetValue(entity, value);
            return entity;
        }

        public static string GetString(int length)
        {
            return new Faker().Random.String2(length);
        }
    }
}
