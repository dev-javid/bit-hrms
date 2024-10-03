using Application.Common.MediatR;

namespace Application.JobTitles.Commands
{
    public class AddJobTitleCommand : IAddCommand<int>
    {
        public int DepartmentId { get; set; }

        public string Name { get; set; } = null!;

        public class Validator : AbstractValidator<AddJobTitleCommand>
        {
            public Validator()
            {
                RuleFor(x => x.Name)
                    .NotEmpty()
                    .MaximumLength(50);

                RuleFor(x => x.DepartmentId)
                    .GreaterThan(0);
            }
        }

        internal class Handler(IDbContext dbContext, ICurrentUser currentUser) : IAddCommandHandler<AddJobTitleCommand, int>
        {
            public async Task<int> Handle(AddJobTitleCommand request, CancellationToken cancellationToken)
            {
                var company = await dbContext
                    .Companies
                    .Include(x => x.Departments.Where(x => x.Id == request.DepartmentId))
                        .ThenInclude(x => x.JobTitles)
                    .FirstAsync(x => x.Id == currentUser.CompanyId, cancellationToken);

                var jobTitle = company.AddJobTitle(request.Name, request.DepartmentId);
                await dbContext.SaveChangesAsync(cancellationToken);
                return jobTitle.Id;
            }
        }
    }
}
