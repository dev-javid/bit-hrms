namespace Application.Companies.Queries
{
    public class GetCompaniesQuery : PagedQuery<GetCompaniesQuery.Response>
    {
        public class Response
        {
            public required int CompanyId { get; set; }

            public required string Name { get; set; }

            public required string PhoneNumber { get; set; }

            public required int FinancialMonth { get; set; }

            public required string AdministratorName { get; set; }

            public required string Email { get; set; }
        }

        internal class Handler(IDbContext dbContext) : IRequestHandler<GetCompaniesQuery, PagedResponse<Response>>
        {
            public async Task<PagedResponse<Response>> Handle(GetCompaniesQuery request, CancellationToken cancellationToken)
            {
                return await dbContext.Companies
                   .Where(x => !x.IsDeleted)
                   .Select(x => new Response
                   {
                       CompanyId = x.Id,
                       Name = x.Name,
                       AdministratorName = x.AdministratorName,
                       Email = x.Email.Value,
                       FinancialMonth = x.FinancialMonth.Value,
                       PhoneNumber = x.PhoneNumber.Value,
                   })
                   .OrderBy(x => x.CompanyId)
                   .ToPagedResponseAsync(request, cancellationToken);
            }
        }
    }
}
