using Application.Common.MediatR;

namespace Application.EmployeeLeaves.Command
{
    public class DeclineLeaveCommand : IUpdateCommand
    {
        [JsonIgnore]
        public int EmployeeLeaveId { get; set; }

        public string Remarks { get; set; } = null!;

        public class Validator : AbstractValidator<DeclineLeaveCommand>
        {
            public Validator()
            {
                RuleFor(x => x.EmployeeLeaveId)
                    .GreaterThan(0);

                RuleFor(x => x.Remarks)
                    .NotEmpty()
                    .MaximumLength(200);
            }
        }

        internal class Handler(IDbContext dbContext) : IUpdateCommandHandler<DeclineLeaveCommand>
        {
            public async Task<bool> Handle(DeclineLeaveCommand request, CancellationToken cancellationToken)
            {
                var leave = await dbContext.EmployeeLeaves
                    .FirstOrDefaultAsync(x => x.Id == request.EmployeeLeaveId, cancellationToken);

                leave?.Decline(request.Remarks);

                return await dbContext.SaveChangesAsync(cancellationToken) > 0;
            }
        }
    }
}
