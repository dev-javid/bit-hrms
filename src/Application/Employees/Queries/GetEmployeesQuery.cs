namespace Application.Employees.Queries
{
    public class GetEmployeesQuery : PagedQuery<GetEmployeeQuery.Response>
    {
        internal class Handler(IDbContext dbContext, IFileService fileService) : IRequestHandler<GetEmployeesQuery, PagedResponse<GetEmployeeQuery.Response>>
        {
            public async Task<PagedResponse<GetEmployeeQuery.Response>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
            {
                var response = await dbContext.Employees
                   .Select(x => new GetEmployeeQuery.Response
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
                       Documents = x.EmployeeDocuments.Select(x => new GetEmployeeQuery.Response.DocumentResponse
                       {
                           Type = x.DocumentType.ToString(),
                           Url = x.FileName.Value,
                       })
                   })
                   .OrderBy(x => x.EmployeeId)
                   .ToPagedResponseAsync(request, cancellationToken);

                response.Items.ToList().ForEach(e =>
                {
                    e.Documents.ToList().ForEach(d =>
                    {
                        d.Url = fileService.GetFileUrl(d.Url.ToValueObject<FileName>(), $"Employees/{e.EmployeeId}");
                    });
                });

                return response;
            }
        }
    }
}
