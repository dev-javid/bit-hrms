namespace Application.Common.MediatR
{
    public class CommandBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var response = await next();

            if (response is MediatRResponse mediatRResponse && mediatRResponse.Error != null)
            {
                throw CustomException.WithInternalServer(mediatRResponse.Error);
            }

            return response;
        }
    }
}
