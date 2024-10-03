using Application.Common.MediatR;

namespace Application.Departments.Commands
{
    public class AddDepartmentCommand : IAddCommand<int>
    {
        public string Name { get; set; } = null!;

        public class Validator : AbstractValidator<AddDepartmentCommand>
        {
            public Validator()
            {
                RuleFor(x => x.Name)
                    .NotEmpty()
                    .MinimumLength(2)
                    .MaximumLength(50);
            }
        }

        internal class Handler(IDbContext dbContext, ICurrentUser currentUser) : IAddCommandHandler<AddDepartmentCommand, int>
        {
            public async Task<int> Handle(AddDepartmentCommand request, CancellationToken cancellationToken)
            {
                var company = await dbContext
                    .Companies
                    .Include(x => x.Departments)
                    .FirstAsync(x => x.Id == currentUser.CompanyId, cancellationToken);

                var department = company.AddDepartment(request.Name);
                await dbContext.SaveChangesAsync(cancellationToken);
                return department.Id;
            }
        }
    }
}
