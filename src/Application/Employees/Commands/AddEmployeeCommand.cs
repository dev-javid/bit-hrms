using Application.Common.MediatR;
using Application.Users.Commands;
using Domain.Employees;

namespace Application.Employees.Commands
{
    public class AddEmployeeCommand : IAddCommand<int>
    {
        public int DepartmentId { get; set; }

        public int JobTitleId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public DateOnly DateOfBirth { get; set; }

        public DateOnly DateOfJoining { get; set; }

        public string FatherName { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string CompanyEmail { get; set; } = null!;

        public string PersonalEmail { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string City { get; set; } = null!;

        public string PAN { get; set; } = null!;

        public string Aadhar { get; set; } = null!;

        public class Validator : AbstractValidator<AddEmployeeCommand>
        {
            public Validator(IDbContext dbContext)
            {
                RuleFor(x => x.DepartmentId)
                    .GreaterThan(0);

                RuleFor(x => x.JobTitleId)
                    .GreaterThan(0);

                RuleFor(x => x.FirstName)
                    .NotEmpty();

                RuleFor(x => x.LastName)
                    .NotEmpty();

                RuleFor(x => x.DateOfBirth)
                    .NotEmpty();

                RuleFor(x => x.DateOfJoining)
                    .NotEmpty();

                RuleFor(x => x.FatherName)
                    .NotEmpty();

                RuleFor(x => x.PhoneNumber)
                    .NotEmpty()
                    .Must(phoneNumber =>
                        {
                            return !dbContext.Users.Any(u => u.PhoneNumber == phoneNumber);
                        })
                    .WithMessage("The phone number is already associated with an existing user.");

                RuleFor(x => x.CompanyEmail)
                    .NotEmpty();

                RuleFor(x => x.PersonalEmail)
                    .NotEmpty();

                RuleFor(x => x.Address)
                    .NotEmpty();

                RuleFor(x => x.City)
                    .NotEmpty();

                RuleFor(x => x.PAN)
                    .NotEmpty();

                RuleFor(x => x.Aadhar)
                    .NotEmpty();
            }
        }

        internal class Handler(IDbContext dbContext, IMediator mediator) : IAddCommandHandler<AddEmployeeCommand, int>
        {
            public async Task<int> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
            {
                var addUserCommand = new AddUserCommand
                {
                    Email = request.CompanyEmail,
                    Name = $"{request.FirstName} {request.LastName}",
                    PhoneNumber = request.PhoneNumber,
                    Role = RoleName.Employee.ToString(),
                    UseTransaction = false,
                };

                using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    var userId = await mediator.Send(addUserCommand, cancellationToken);

                    var employee = Employee.Create(
                        userId,
                        request.DepartmentId,
                        request.JobTitleId,
                        request.FirstName,
                        request.LastName,
                        request.FatherName,
                        request.DateOfBirth,
                        request.DateOfJoining,
                        request.PhoneNumber.ToValueObject<PhoneNumber>(),
                        request.CompanyEmail.ToValueObject<Email>(),
                        request.PersonalEmail.ToValueObject<Email>(),
                        request.Address,
                        request.City,
                        request.PAN.ToValueObject<PAN>(),
                        request.Aadhar.ToValueObject<Aadhar>());

                    await dbContext.Employees.AddAsync(employee, cancellationToken);
                    await dbContext.SaveChangesAsync(cancellationToken);

                    await transaction.CommitAsync(cancellationToken);

                    return employee.Id;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    throw;
                }
            }
        }
    }
}
