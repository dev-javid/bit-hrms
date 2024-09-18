namespace Application.Common.Abstract
{
    public interface IEmailService
    {
        void Send(Email reciever, string subject, string htmlBody);
    }
}
