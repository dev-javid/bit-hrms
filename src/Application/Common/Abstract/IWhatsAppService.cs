namespace Application.Common.Abstract;

public interface IWhatsAppService
{
    Task SendAsync(string templateName, object[] variables, CancellationToken cancellationToken);
}
