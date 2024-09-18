namespace Application.Common.Abstract
{
    internal interface IAuthorizeRequest
    {
        Task<bool> AuthorizeAsync(ICurrentUser currentUser, IDbContext dbContext, CancellationToken cancellationToken);
    }
}
