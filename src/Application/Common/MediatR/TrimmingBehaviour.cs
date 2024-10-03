namespace Application.Common.MediatR;

public class TrimmingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var stringProperties = request.GetType()
            .GetProperties()
            .Where(x => x.PropertyType == typeof(string));

        foreach (var propertyInfo in stringProperties)
        {
            var currentValue = (string?)propertyInfo.GetValue(request);
            if (currentValue != null)
            {
                propertyInfo.SetValue(request, currentValue.Trim());
            }
        }

        return await next();
    }
}
