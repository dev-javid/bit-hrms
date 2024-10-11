using Application.Common.MediatR;

namespace Application.Holidays.Commands
{
    public class UpdateHolidayCommand : IUpdateCommand
    {
        [JsonIgnore]
        public int HolidayId { get; set; }

        public string Name { get; set; } = null!;

        public DateOnly Date { get; set; }

        public bool Optional { get; set; }

        public class Validator : AbstractValidator<UpdateHolidayCommand>
        {
            public Validator()
            {
                RuleFor(x => x.HolidayId)
                    .GreaterThan(0);

                RuleFor(x => x.Name)
                    .NotEmpty()
                    .MaximumLength(50);
            }
        }

        internal class Handler(IDbContext dbContext, ICurrentUser currentUser) : IUpdateCommandHandler<UpdateHolidayCommand>
        {
            public async Task<bool> Handle(UpdateHolidayCommand request, CancellationToken cancellationToken)
            {
                var company = await dbContext
                    .Companies
                    .Include(x => x.Holidays)
                    .Include(x => x.LeavePolicy)
                    .FirstAsync(x => x.Id == currentUser.CompanyId, cancellationToken);

                company.UpdateHoliday(request.HolidayId, request.Name, request.Date, request.Optional);
                return await dbContext.SaveChangesAsync(cancellationToken) > 0;
            }
        }
    }
}
