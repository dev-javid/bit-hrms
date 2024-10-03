using Application.Common.MediatR;

namespace Application.Companies.Commands
{
    public abstract class AddUpdateCommand : IMediatRCommand<int>
    {
        public string Name { get; set; } = null!;

        public string AdministratorName { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string Address { get; set; } = null!;

        public class Validator : AbstractValidator<AddUpdateCommand>
        {
            public Validator()
            {
                RuleFor(x => x.Name)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(100);

                RuleFor(x => x.Address)
                    .NotEmpty()
                    .MinimumLength(30)
                    .MaximumLength(200);

                RuleFor(x => x.PhoneNumber)
                    .NotEmpty();

                RuleFor(x => x.AdministratorName)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(100);
            }
        }
    }
}
