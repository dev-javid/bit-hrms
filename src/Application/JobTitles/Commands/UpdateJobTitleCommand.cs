using Application.Common.MediatR;

namespace Application.JobTitles.Commands
{
    public class UpdateJobTitleCommand : IUpdateCommand
    {
        public int JobTitleId { get; set; }

        public string Name { get; set; } = null!;

        public class Validator : AbstractValidator<UpdateJobTitleCommand>
        {
            public Validator()
            {
                RuleFor(x => x.Name)
                    .NotEmpty()
                    .MaximumLength(50);

                RuleFor(x => x.JobTitleId)
                    .GreaterThan(0);
            }
        }

        internal class Handler(IDbContext dbContext, ICurrentUser currentUser) : IUpdateCommandHandler<UpdateJobTitleCommand>
        {
            public async Task<bool> Handle(UpdateJobTitleCommand request, CancellationToken cancellationToken)
            {
                var company = await dbContext
                    .Companies
                    .Include(x => x.Departments.Where(d => d.JobTitles.Any(j => j.Id == request.JobTitleId)))
                        .ThenInclude(x => x.JobTitles)
                    .FirstAsync(x => x.Id == currentUser.CompanyId, cancellationToken);

                company.UpdateJobTitle(request.JobTitleId, request.Name);
                return await dbContext.SaveChangesAsync(cancellationToken) > 0;
            }
        }
    }
}
