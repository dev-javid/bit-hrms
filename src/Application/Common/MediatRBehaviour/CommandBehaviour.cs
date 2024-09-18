namespace Application.Common.MediatRBehaviour
{
    public class CommandBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : struct
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                var response = await next();

                if (response is bool boolResponse)
                {
                    if (!boolResponse)
                    {
                        throw CustomException.WithInternalServer("Unable to process the request. The system state was not changed.");
                    }
                }
                else if (response.GetType() == typeof(TResponse))
                {
                    if (EqualityComparer<TResponse>.Default.Equals(response, default))
                    {
                        throw CustomException.WithInternalServer("Unable to process the request. The system state was not changed.");
                    }
                }
                else
                {
                    throw new InvalidOperationException("Unexpected response type.");
                }

                return response;
            }
            catch (Exception ex)
            {
                throw CustomException.WithInternalServer($"An error occurred while processing the request: {ex.Message}");
            }
        }
    }
}
