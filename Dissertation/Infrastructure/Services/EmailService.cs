using System.Net;
using System.Net.Mail;
using Dissertation.Common.Services;
using Microsoft.Extensions.Options;
using Dissertation.Infrastructure.Common;

namespace Dissertation.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly EmailHostSettings _emailSettings;

    public EmailService(IOptionsMonitor<EmailHostSettings> options)
        => (_emailSettings) = (options.CurrentValue);

    public async Task SendAsync(MailMessage mail)
    {
        using var client = new SmtpClient(_emailSettings.Host, _emailSettings.Port)
        {
            Credentials = new NetworkCredential(_emailSettings.EmailId, _emailSettings.Password),
            EnableSsl = _emailSettings.UseSSL
        };

        await client.SendMailAsync(mail, new CancellationToken());
    }
}
