using System.Text;
using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace e_commerce.Services.Email;

public class EmailService : IEmailService
{
    private readonly EmailOptions _opt;

    public EmailService(IOptions<EmailOptions> opt)
    {
        _opt = opt.Value;
    }

    public async Task SendAsync(string toEmail, string subject, string htmlBody)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(_opt.FromName, _opt.FromEmail));
        message.To.Add(MailboxAddress.Parse(toEmail));
        message.Subject = subject;
        message.Body = new BodyBuilder { HtmlBody = htmlBody }.ToMessageBody();

        var socketOptions =
            _opt.UseSsl ? SecureSocketOptions.SslOnConnect :
            _opt.UseStartTls ? SecureSocketOptions.StartTls :
            SecureSocketOptions.None;

        using var logStream = new MemoryStream();
        using var logger = new ProtocolLogger(logStream);
        using var smtp = new SmtpClient(logger);

        smtp.CheckCertificateRevocation = false;

        try
        {
            await smtp.ConnectAsync(_opt.SmtpHost, _opt.SmtpPort, socketOptions);
            await smtp.AuthenticateAsync(_opt.Username, _opt.Password);
            await smtp.SendAsync(message);
            await smtp.DisconnectAsync(true);
        }
        catch (Exception ex)
        {
            // اقرأ اللوج من الذاكرة
            logStream.Position = 0;
            var smtpLog = new StreamReader(logStream, Encoding.UTF8, leaveOpen: true).ReadToEnd();

            // حاول تطلع تفاصيل SMTP لو متوفرة
            if (ex is SmtpCommandException cmdEx)
            {
                throw new Exception(
                    $"SMTP ERROR (Command)\n" +
                    $"StatusCode: {cmdEx.StatusCode}\n" +
                    $"ErrorCode: {cmdEx.ErrorCode}\n" +
                    $"SMTP LOG:\n{smtpLog}\n\n" +
                    $"EX:\n{ex}",
                    ex
                );
            }

            if (ex is SmtpProtocolException)
            {
                throw new Exception(
                    $"SMTP ERROR (Protocol)\n\nSMTP LOG:\n{smtpLog}\n\nEX:\n{ex}",
                    ex
                );
            }

            throw new Exception(
                $"SMTP ERROR (General)\n\nSMTP LOG:\n{smtpLog}\n\nEX:\n{ex}",
                ex
            );
        }
    }
}
