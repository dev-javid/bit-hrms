namespace Application.Common.MediatR;

public class ValidationBehaviour<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            await Task.WhenAll(
                validators.Select(v =>
                    v.ValidateAsync(context, cancellationToken)));

            var validationFailures = validators
               .Select(async validator => await validator.ValidateAsync(new ValidationContext<TRequest>(request)))
               .SelectMany(validationResult => validationResult.Result.Errors)
               .Where(validationFailure => validationFailure != null)
               .ToList();

            if (validationFailures.Any())
            {
                throw new ValidationException("Bad request", validationFailures);
            }
        }

        return await next();
    }
}
