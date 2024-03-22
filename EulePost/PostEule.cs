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
    private readonly SecureSocketOptions m_sso;

    public PostEule(EulePostSettings settings)
    {
        m_emailAddress = settings.EmailAddress;
        m_password = settings.Password;
        m_host = settings.Host;
        m_port = settings.Port;

        m_sso = settings.SSO switch
        {
            "SSL" => SecureSocketOptions.SslOnConnect,
            "TLS" => SecureSocketOptions.StartTls,
            _ => SecureSocketOptions.None
        };
    }

    private async Task SendAsync(MimeMessage message)
    {
        using (SmtpClient smtp = new())
        {
            await smtp.ConnectAsync(m_host, m_port, options: m_sso);
            await smtp.AuthenticateAsync(m_emailAddress, m_password);
            await smtp.SendAsync(message);
            await smtp.DisconnectAsync(true);
        }
    }

    public async Task SendAsync(MailContactCollection contacts, string subject, StringBuilder body)
    {
        if (!IsValid())
            return;

        MimeMessage message = new();

        message.SetMessageContacts(contacts);

        message.Subject = subject;

        message.SetBody(body);

        await SendAsync(message);
    }

    public async Task SendAsync(MailContactCollection contacts, string subject, StringBuilder body, IAttachment[] attachments)
    {
        if (!IsValid())
            return;

        MimeMessage message = new();

        message.SetMessageContacts(contacts);
        
        message.Subject = subject;

        message.SetBody(body, attachments);

        await SendAsync(message);
    }

    private bool IsValid()
    {
        string[] emailParts = m_emailAddress.Split('@');
        if (emailParts.Length != 2)
            return false;

        if (emailParts[1].IndexOf('.') == -1) 
            return false;

        if(string.IsNullOrWhiteSpace(m_password))
            return false;


        return true;
    }
}
