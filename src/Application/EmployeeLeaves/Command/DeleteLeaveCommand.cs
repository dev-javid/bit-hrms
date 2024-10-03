using Application.Common.MediatR;

namespace Application.EmployeeLeaves.Command
{
    public class DeleteLeaveCommand : IUpdateCommand, IAuthorizeRequest
    {
        public int EmployeeLeaveId { get; set; }

        public async Task<bool> AuthorizeAsync(ICurrentUser currentUser, IDbContext dbContext, CancellationToken cancellationToken)
        {
            return await dbContext.EmployeeLeaves.AnyAsync(x => x.Id == EmployeeLeaveId, cancellationToken);
        }

        public class Validator : AbstractValidator<DeleteLeaveCommand>
        {
            public Validator()
            {
                RuleFor(x => x.EmployeeLeaveId)
                    .GreaterThan(0);
            }
        }

        internal class Handler(IDbContext dbContext, ICurrentUser currentUser) : IUpdateCommandHandler<DeleteLeaveCommand>
        {
            public async Task<bool> Handle(DeleteLeaveCommand request, CancellationToken cancellationToken)
            {
                var employee = await dbContext.Employees
                    .Include(x => x.EmployeeLeaves.Where(x => x.Id == request.EmployeeLeaveId))
                    .FirstOrDefaultAsync(x => x.Id == currentUser.EmployeeId, cancellationToken);

                employee?.DeleteLeave(request.EmployeeLeaveId);

                return await dbContext.SaveChangesAsync(cancellationToken) > 0;
            }
        }
    }
}
