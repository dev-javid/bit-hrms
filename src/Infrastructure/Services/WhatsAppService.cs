using System.Text;
using System.Text.Json.Serialization;
using Application.Common.Configuration;

namespace Infrastructure.Services;

public class WhatsAppService(IHttpClientFactory httpClientFactory, WhatsAppConfiguration whatsAppConfig) : IWhatsAppService
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("WhatsApp");

    public async Task SendAsync(string templateName, object[] variables, CancellationToken cancellationToken)
    {
        var path = $"{whatsAppConfig.CloudApiversion}/{whatsAppConfig.WhatsAppPhoneNumberId}/messages";

        var json = JsonSerializer.Serialize(CreateRequest(templateName, variables));
        using var content = new StringContent(json, Encoding.UTF8, "application/json");
        using var request = new HttpRequestMessage(HttpMethod.Post, path);
        request.Content = content;
        await _httpClient.SendAsync(request, cancellationToken);
    }

    private WhatsAppMessageRequest CreateRequest(string templateName, object[] variables)
    {
        return new WhatsAppMessageRequest
        {
            MessagingProduct = "whatsapp",
            To = $"{whatsAppConfig.TargetPhoneNumber}",
            Type = "template",
            Template = new WhatsAppMessageRequest.WhatsAppTemplate
            {
                Name = $"{templateName}",
                Language = new WhatsAppMessageRequest.WhatsAppTemplate.TemplateLanguage
                {
                    Code = "en"
                },
                Components =
                [
                    new()
                    {
                        Type = "body",
                        Parameters = variables.Select(x => new WhatsAppMessageRequest.WhatsAppTemplate.Component.Parameter("text", x.ToString()!))
                    }

                ]
            }
        };
    }

    private sealed class WhatsAppMessageRequest
    {
        [JsonPropertyName("messaging_product")]
        public string MessagingProduct { get; set; } = null!;

        [JsonPropertyName("to")]
        public string To { get; set; } = null!;

        [JsonPropertyName("type")]
        public string Type { get; set; } = null!;

        [JsonPropertyName("template")]
        public WhatsAppTemplate Template { get; set; } = null!;

        public class WhatsAppTemplate
        {
            [JsonPropertyName("name")]
            public string Name { get; set; } = null!;

            [JsonPropertyName("language")]
            public TemplateLanguage Language { get; set; } = null!;

            [JsonPropertyName("components")]
            public List<Component> Components { get; set; } = null!;

            public class TemplateLanguage
            {
                [JsonPropertyName("code")]
                public string Code { get; set; } = null!;
            }

            public class Component
            {
                [JsonPropertyName("type")]
                public string Type { get; set; } = null!;

                [JsonPropertyName("parameters")]
                public IEnumerable<Parameter> Parameters { get; set; } = null!;

                public class Parameter(string type, string text)
                {
                    [JsonPropertyName("type")]
                    public string Type { get; set; } = type;

                    [JsonPropertyName("text")]
                    public string Text { get; set; } = text;
                }
            }
        }
    }
}
