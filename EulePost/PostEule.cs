using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System.Text;

namespace EulePost;

public sealed class PostEule
{
    private readonly string m_emailAddress;
    private readonly string m_password;
    private readonly string m_host;
    private readonly int m_port;

    public PostEule(EulePostSettings settings)
    {
        m_emailAddress = settings.EmailAddress;
        m_password = settings.Password;
        m_host = settings.Host;
        m_port = settings.Port;
    }

    private async Task SendAsync(MimeMessage message)
    {
        using (SmtpClient smtp = new())
        {
            await smtp.ConnectAsync(m_host, m_port, SecureSocketOptions.SslOnConnect);
            await smtp.AuthenticateAsync(m_emailAddress, m_password);
            await smtp.SendAsync(message);
            await smtp.DisconnectAsync(true);
        }
    }

    public async Task SendAsync(MailContactCollection contacts, string subject, StringBuilder body)
    {
        MimeMessage message = new();

        message.SetMessageContacts(contacts);

        message.Subject = subject;

        message.SetBody(body);

        await SendAsync(message);
    }

    public async Task SendAsync(MailContactCollection contacts, string subject, StringBuilder body, IAttachment[] attachments)
    {
        MimeMessage message = new();

        message.SetMessageContacts(contacts);
        
        message.Subject = subject;

        message.SetBody(body, attachments);

        await SendAsync(message);
    }
}
