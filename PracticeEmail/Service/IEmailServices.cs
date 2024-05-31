using PracticeEmail.Helpper;

namespace PracticeEmail.Service
{
    public interface IEmailServices
    {
     Task SendEmailAsync(MailRequest request);
    }
}
