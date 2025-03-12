using Application.Common.Services;
using System.Net;
using System.Net.Mail;

namespace Infrastructure.Services;

public class EmailSender : IEmailSender
{
    public async Task SendEmailAsync(string email, string subject, string message, CancellationToken cancellationToken = default)
    {
        var mail = "cinematogether01@gmail.com";
        var password = "toox xuhp twvr dafx";

        var client = new SmtpClient("smtp.gmail.com", 587)
        {
            EnableSsl = true,
            Credentials = new NetworkCredential(mail, password),
            UseDefaultCredentials = false
        };
        
        var msg = new MailMessage(mail, email, subject, message);
        msg.IsBodyHtml = true;

        await client.SendMailAsync(msg, cancellationToken);
        
        client.Dispose();
    }
}