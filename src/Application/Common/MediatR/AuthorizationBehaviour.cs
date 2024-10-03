namespace Application.Common.MediatR
{
    internal class AuthorizationBehaviour<TRequest, TResponse>(ICurrentUser currentUser, IDbContext dbContext) : IPipelineBehavior<TRequest, TResponse>
         where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is IAuthorizeRequest authRequest)
            {
                var authorized = await authRequest.AuthorizeAsync(currentUser, dbContext, cancellationToken);
                if (!authorized)
                {
                    throw CustomException.WithForbidden("You are not authorized to access this resource.");
                }
            }

            return await next();
        }
    }
}
