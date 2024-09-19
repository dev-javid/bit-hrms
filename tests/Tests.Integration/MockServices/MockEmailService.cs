using Application.Common.Abstract;
using Domain.Common.ValueObjects;

namespace Tests.Integration.MockServices
{
    internal class MockEmailService : IEmailService
    {
        public void Send(Email reciever, string subject, string htmlBody)
        {
            Console.WriteLine("Email sent");
        }
    }
}
