using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.Common.Abstract
{
    public abstract class PagedQuery<TResponse> : IRequest<PagedResponse<TResponse>>
    {
        [DefaultValue(1)]
        public int Page { get; set; } = 1;

        [DefaultValue(10)]
        public int Limit { get; set; } = 10;
    }

    public class PagedResponse<T>
    {
        public PagedResponse(IEnumerable<T> items, int count, int page, int limit)
        {
            Page = page;
            Limit = limit;
            Total = count;
            Items = items;
        }

        public int Page { get; }

        public int Limit { get; }

        public int Total { get; }

        public IEnumerable<T> Items { get; }
    }
}
