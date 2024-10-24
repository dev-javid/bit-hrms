namespace Application.Employees.Queries
{
    public class GetEmployeesQuery : PagedQuery<GetEmployeesQuery.Response>
    {
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
        }

        internal class Handler(IDbContext dbContext) : IRequestHandler<GetEmployeesQuery, PagedResponse<Response>>
        {
            public async Task<PagedResponse<Response>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
            {
                var response = await dbContext.Employees
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
                       JobTitleId = x.JobTitleId,
                       JobTitleName = x.JobTitle.Name,
                       FirstName = x.FirstName,
                       FullName = x.FullName,
                       LastName = x.LastName,
                       PAN = x.PAN.Value,
                       PersonalEmail = x.PersonalEmail.Value,
                       PhoneNumber = x.PhoneNumber.Value,
                       Compensation = x.Compensations.Any() ? x.Compensations.OrderByDescending(s => s.EffectiveFrom).First().Amount : null,
                   })
                   .OrderBy(x => x.EmployeeId)
                   .ToPagedResponseAsync(request, cancellationToken);

                return response;
            }
        }
    }
}
