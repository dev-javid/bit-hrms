namespace Application.Companies.Queries
{
    public class GetLeavePolicyQuery : IRequest<GetLeavePolicyQuery.Response?>
    {
        public class Response
        {
            public required int LeavePolicyId { get; set; }

            public required int Holidays { get; set; }

            public required int CasualLeaves { get; set; }

            public required double EarnedLeavesPerMonth { get; set; }
        }

        internal class Handler(IDbContext dbContext) : IRequestHandler<GetLeavePolicyQuery, Response?>
        {
            public async Task<Response?> Handle(GetLeavePolicyQuery request, CancellationToken cancellationToken)
            {
                return await dbContext.LeavePolicies
                   .Select(x => new Response
                   {
                       LeavePolicyId = x.Id,
                       Holidays = x.Holidays,
                       CasualLeaves = x.CasualLeaves,
                       EarnedLeavesPerMonth = x.EarnedLeavesPerMonth,
                   })
                   .FirstOrDefaultAsync(cancellationToken);
            }
        }
    }
}
