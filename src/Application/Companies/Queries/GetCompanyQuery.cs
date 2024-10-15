namespace Application.Companies.Queries
{
    public class GetCompanyQuery : IRequest<GetCompanyQuery.Response?>, IAuthorizeRequest
    {
        public int CompanyId { get; set; }

        public Task<bool> AuthorizeAsync(ICurrentUser currentUser, IDbContext dbContext, CancellationToken cancellationToken)
        {
            return Task.FromResult(currentUser.IsSuperAdmin || currentUser.CompanyId == CompanyId);
        }

        public class Response : GetCompaniesQuery.Response
        {
            public required IEnumerable<string> WeeklyOffDays { get; set; }
        }

        internal class Handler(IDbContext dbContext) : IRequestHandler<GetCompanyQuery, Response?>
        {
            public async Task<Response?> Handle(GetCompanyQuery request, CancellationToken cancellationToken)
            {
                var company = await dbContext.Companies
                   .Where(x => x.Id == request.CompanyId && !x.IsDeleted)
                   .Select(x => new
                   {
                       CompanyId = x.Id,
                       x.Name,
                       x.AdministratorName,
                       Email = x.Email.Value,
                       FinancialMonth = x.FinancialMonth.Value,
                       PhoneNumber = x.PhoneNumber.Value,
                       x.WeeklyOffDays,
                       x.Address,
                       CreatedOn = x.CreatedOn.ToDateOnly(),
                   })
                   .OrderBy(x => x.Name)
                   .FirstOrDefaultAsync(cancellationToken);

                return company.Adapt<Response>();
            }
        }
    }
}
