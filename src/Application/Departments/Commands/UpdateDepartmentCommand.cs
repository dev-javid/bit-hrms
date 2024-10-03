using Application.Common.MediatR;

namespace Application.Departments.Commands
{
    public class UpdateDepartmentCommand : IUpdateCommand
    {
        [JsonIgnore]
        public int DepartmentId { get; set; }

        public string Name { get; set; } = null!;

        public class Validator : AbstractValidator<UpdateDepartmentCommand>
        {
            public Validator()
            {
                RuleFor(x => x.DepartmentId)
                    .GreaterThan(0);

                RuleFor(x => x.Name)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(50);
            }
        }

        internal class Handler(IDbContext dbContext, ICurrentUser currentUser) : IUpdateCommandHandler<UpdateDepartmentCommand>
        {
            public async Task<bool> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
            {
                var company = await dbContext
                   .Companies
                   .Include(x => x.Departments)
                   .FirstAsync(x => x.Id == currentUser.CompanyId, cancellationToken);

                company.UpdateDepartment(request.DepartmentId, request.Name);
                return await dbContext.SaveChangesAsync(cancellationToken) > 0;
            }
        }
    }
}
