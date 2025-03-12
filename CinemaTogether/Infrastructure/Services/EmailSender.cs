using Application.Common.Services;
using System.Net;
using System.Net.Mail;

namespace Infrastructure.Services;

public class EmailSender : IEmailSender
{
    public async Task SendEmailAsync(string email, string subject, string message, CancellationToken cancellationToken = default)
    {
        var mail = "";
        var password = "";

        var client = new SmtpClient("smtp.gmail.com", 587)
        {
            EnableSsl = true,
            Credentials = new NetworkCredential(mail, password),
            UseDefaultCredentials = false
        };

        await client.SendMailAsync(mail, email, subject, message, cancellationToken);
        client.Dispose();
    }
}