using System.Text;
using Serilog;

namespace Infrastructure.HttpHandlers
{
    public class LoggingDelegatingHandler : DelegatingHandler
    {
        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            WriteIndented = true
        };

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            StringBuilder stringBuilder = new StringBuilder();
#pragma warning disable S2139 // Exceptions should be either logged or rethrown but not both
            try
            {
                stringBuilder.AppendLine($"HTTP {request.Method} request to {request.RequestUri}");

                if (request.Content != null)
                {
                    string requestBody = await request.Content.ReadAsStringAsync(cancellationToken);
                    stringBuilder.AppendLine($"Request Body:\n{FormatJsonString(requestBody)}");
                }

                var response = await base.SendAsync(request, cancellationToken);

                stringBuilder.AppendLine($"HTTP response status: {(int)response.StatusCode} {response.ReasonPhrase}");

                if (response.Content != null)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    stringBuilder.AppendLine($"Response Body:\n{FormatJsonString(responseBody)}");
                }

                response.EnsureSuccessStatusCode();

                Log.Information(stringBuilder.ToString());

                return response;
            }
            catch (Exception e)
            {
                stringBuilder.AppendLine(e.UnwrapExceptionMessage());
                Log.Error(e, stringBuilder.ToString());
                throw;
            }
#pragma warning restore S2139 // Exceptions should be either logged or rethrown but not both
        }

        private string FormatJsonString(string json)
        {
            try
            {
                var jsonElement = JsonSerializer.Deserialize<JsonElement>(json);
                return JsonSerializer.Serialize(jsonElement, _jsonOptions);
            }
            catch (Exception)
            {
                return json;
            }
        }
    }
}
