using Application.Common.Events;
using Application.Common.MediatR;

namespace Application.Employees.Commands
{
    public class SetEmployeeDocumentCommand : IAddCommand<int>
    {
        [JsonIgnore]
        public int EmployeeId { get; set; }

        public string Type { get; set; } = null!;

        public string Document { get; set; } = null!;

        public class Validator : AbstractValidator<SetEmployeeDocumentCommand>
        {
            public Validator()
            {
                RuleFor(x => x.EmployeeId)
                    .GreaterThan(0);

                RuleFor(x => x.Type)
                    .NotEmpty();

                RuleFor(x => x.Document)
                    .NotEmpty();
            }
        }

        internal class Handler(IDbContext dbContext, IFileService fileService) : IAddCommandHandler<SetEmployeeDocumentCommand, int>
        {
            public async Task<int> Handle(SetEmployeeDocumentCommand request, CancellationToken cancellationToken)
            {
                var employee = await dbContext.Employees
                    .Include(x => x.EmployeeDocuments)
                    .FirstAsync(x => x.Id == request.EmployeeId, cancellationToken);

                var employeeDirectory = $"Employees/{employee.Id}";
                var fileName = await fileService.SaveBase64StringAsFileAsync(request.Document, employeeDirectory, cancellationToken);
                try
                {
                    var currentDocument = employee
                        .EmployeeDocuments
                        .FirstOrDefault(x => x.DocumentType == request.Type.AsEnum<DocumentType>());

                    if (currentDocument != null)
                    {
                        employee.AddDomainEvent(new DocumentMarkedAsDeletedEvent
                        {
                            FileName = currentDocument.FileName.Value,
                            RelativePath = employeeDirectory,
                            RetryOnError = true
                        });
                    }

                    var document = employee.SetDocument(fileName, request.Type.AsEnum<DocumentType>());
                    await dbContext.SaveChangesAsync(cancellationToken);
                    return document.Id;
                }
                catch (Exception)
                {
                    fileService.DeleteFile(fileName, employeeDirectory);
                    throw;
                }
            }
        }
    }
}
