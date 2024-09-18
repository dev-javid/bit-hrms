using Application.Departments.Commands;

namespace Application.Holidays.Commands
{
    public class AddHolidayCommand : IAddCommand<int>
    {
        public required string Name { get; set; }

        public required DateOnly Date { get; set; }

        public bool Optional { get; set; }

        public class Validator : AbstractValidator<AddHolidayCommand>
        {
            public Validator()
            {
                RuleFor(x => x.Name)
                    .NotEmpty()
                    .MaximumLength(50);
            }
        }

        internal class Handler(IDbContext dbContext, ICurrentUser currentUser) : IAddCommandHandler<AddHolidayCommand, int>
        {
            public async Task<int> Handle(AddHolidayCommand request, CancellationToken cancellationToken)
            {
                var company = await dbContext
                    .Companies
                    .Include(x => x.Holidays)
                    .FirstAsync(x => x.Id == currentUser.CompanyId, cancellationToken);

                var holiday = company.AddHoliday(request.Name, request.Date, request.Optional);
                await dbContext.SaveChangesAsync(cancellationToken);
                return holiday.Id;
            }
        }
    }
}
