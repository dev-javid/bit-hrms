namespace Application.Employees.Queries
{
    public class GetEmployeeQuery : IRequest<GetEmployeeQuery.Response?>, IAuthorizeRequest
    {
        public int EmployeeId { get; set; }

        public Task<bool> AuthorizeAsync(ICurrentUser currentUser, IDbContext dbContext, CancellationToken cancellationToken)
        {
            return Task.FromResult(currentUser.IsCompanyAdmin || currentUser.EmployeeId == EmployeeId);
        }

        public class Response
        {
            public required int EmployeeId { get; set; }

            public required int DepartmentId { get; set; }

            public required string DepartmentName { get; set; }

            public required int JobTitleId { get; set; }

            public required string JobTitleName { get; set; }

            public required string FirstName { get; set; }

            public required string LastName { get; set; }

            public required string FullName { get; set; }

            public required DateOnly DateOfBirth { get; set; }

            public required DateOnly DateOfJoining { get; set; }

            public required string FatherName { get; set; }

            public required string PhoneNumber { get; set; }

            public required string CompanyEmail { get; set; }

            public required string PersonalEmail { get; set; }

            public required string Address { get; set; }

            public required string City { get; set; }

            public required string PAN { get; set; }

            public required string Aadhar { get; set; }

            public required decimal? Compensation { get; set; }

            public required IEnumerable<DocumentResponse> Documents { get; set; }

            public class DocumentResponse
            {
                public required string Type { get; set; }

                public required string Url { get; set; }

                public required DateTime UpdatedOn { get; set; }
            }
        }

        public class Validator : AbstractValidator<GetEmployeeQuery>
        {
            public Validator()
            {
                RuleFor(x => x.EmployeeId)
                    .GreaterThan(0);
            }
        }

        internal class Handler(IDbContext dbContext, IFileService fileService) : IRequestHandler<GetEmployeeQuery, Response?>
        {
            public async Task<Response?> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
            {
                return await dbContext.Employees
                   .AsSplitQuery()
                   .Select(x => new Response
                   {
                       EmployeeId = x.Id,
                       Aadhar = x.Aadhar.Value,
                       Address = x.Address,
                       City = x.City,
                       CompanyEmail = x.CompanyEmail.Value,
                       DateOfBirth = x.DateOfBirth,
                       DateOfJoining = x.DateOfJoining,
                       DepartmentId = x.DepartmentId,
                       DepartmentName = x.Department.Name,
                       FatherName = x.FatherName,
                       FirstName = x.FirstName,
                       LastName = x.LastName,
                       FullName = x.FullName,
                       JobTitleId = x.JobTitleId,
                       JobTitleName = x.JobTitle.Name,
                       PAN = x.PAN.Value,
                       PersonalEmail = x.PersonalEmail.Value,
                       PhoneNumber = x.PhoneNumber.Value,
                       Compensation = x.Compensations.Any() ? x.Compensations.OrderByDescending(s => s.EffectiveFrom).First().Amount : null,
                       Documents = x.EmployeeDocuments.Select(x => new Response.DocumentResponse
                       {
                           Type = x.DocumentType.ToString(),
                           Url = fileService.GetFileUrl(x.FileName, $"Employees/{x.EmployeeId}"),
                           UpdatedOn = x.ModifiedOn != null ? x.ModifiedOn.Value : x.CreatedOn
                       })
                   })
                   .FirstOrDefaultAsync(x => x.EmployeeId == request.EmployeeId, cancellationToken);
            }
        }
    }
}
