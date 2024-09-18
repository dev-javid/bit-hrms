namespace Application.Common.Extensions
{
    internal static class QueryableExtensions
    {
        public static async Task<PagedResponse<T>> ToPagedResponseAsync<T>(this IQueryable<T> queryable, PagedQuery<T> query, CancellationToken cancellationToken)
        {
            var count = await queryable.CountAsync(cancellationToken);
            var items = await queryable.Skip((query.Page - 1) * query.Limit).Take(query.Limit).ToListAsync(cancellationToken);
            return new PagedResponse<T>(items, count, query.Page, query.Limit);
        }
    }
}
