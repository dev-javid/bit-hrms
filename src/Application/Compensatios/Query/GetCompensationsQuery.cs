namespace Application.Compensatios.Query;

public class GetCompensationsQuery : IRequest<IEnumerable<GetCompensationsQuery.Response>>, IAuthorizeRequest
{
    public int? EmployeeId { get; set; }

    public Task<bool> AuthorizeAsync(ICurrentUser currentUser, IDbContext dbContext, CancellationToken cancellationToken)
    {
        return Task.FromResult(currentUser.IsCompanyAdmin || !EmployeeId.HasValue || currentUser.EmployeeId == EmployeeId);
    }

    public class Validator : AbstractValidator<GetCompensationsQuery>
    {
        public Validator()
        {
            RuleFor(x => x.EmployeeId)
                .GreaterThan(0);
        }
    }

    public class Response
    {
        public required int CompensationId { get; set; }

        public required int EmployeeId { get; set; }

        public required DateOnly EffectiveFrom { get; set; }

        public required decimal Amount { get; set; }
    }

    internal class Handler(IDbContext dbContext, ICurrentUser currentUser) : IRequestHandler<GetCompensationsQuery, IEnumerable<Response>>
    {
        public async Task<IEnumerable<Response>> Handle(GetCompensationsQuery request, CancellationToken cancellationToken)
        {
            var employeeId = request.EmployeeId ?? currentUser.EmployeeId;

            return await dbContext.Compensations
               .Where(x => x.EmployeeId == employeeId)
               .Select(x => new Response
               {
                   CompensationId = x.Id,
                   EmployeeId = x.EmployeeId,
                   EffectiveFrom = x.EffectiveFrom,
                   Amount = x.Amount
               })
               .ToListAsync(cancellationToken);
        }
    }
}
