namespace Infrastructure.HttpHandlers
{
    public class AuthenticationDelegatingHandler(string accessToken) : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("Authorization", $"Bearer {accessToken}");
            return base.SendAsync(request, cancellationToken);
        }
    }
}
