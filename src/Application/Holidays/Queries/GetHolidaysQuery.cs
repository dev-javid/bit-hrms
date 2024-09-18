namespace Application.Holidays.Queries
{
    public class GetHolidaysQuery : PagedQuery<GetHolidaysQuery.Response>
    {
        public class Response
        {
            public required int HolidayId { get; set; }

            public required DateOnly Date { get; set; }

            public required string Name { get; set; }

            public required bool Optional { get; set; }
        }

        internal class Handler(IDbContext dbContext) : IRequestHandler<GetHolidaysQuery, PagedResponse<Response>>
        {
            public async Task<PagedResponse<Response>> Handle(GetHolidaysQuery request, CancellationToken cancellationToken)
            {
                return await dbContext.Holidays
                   .Select(x => new Response
                   {
                       HolidayId = x.Id,
                       Name = x.Name,
                       Date = x.Date,
                       Optional = x.Optional
                   })
                   .OrderBy(x => x.Date)
                   .ToPagedResponseAsync(request, cancellationToken);
            }
        }
    }
}
