using System.Net.Mail;

namespace Dissertation.Common.Services;

public interface IEmailService
{
    Task SendAsync(MailMessage mail);
}
