using MimeKit;
using System.Text;

namespace EulePost;

public static class Extension
{
    public static void SetMessageContacts(this MimeMessage msg, MailContactCollection contacts)
    {
        if (!contacts.IsValid())
            throw new FormatException("Contacts collection is not valid");

        foreach(MailContact contact in contacts.ToList())
        {
            if(contact.Type == MailContact.ContactType.From)
            {
                msg.From.Add(contact.ToAddress());
                continue;
            }

            if(contact.Type == MailContact.ContactType.CC)
            {
                msg.Cc.Add(contact.ToAddress());
                continue;
            }

            if(contact.Type == MailContact.ContactType.BCC)
            {
                msg.Bcc.Add(contact.ToAddress());
                continue;
            }

            msg.To.Add(contact.ToAddress());
        }
    }

    public static void SetBody(this MimeMessage msg, StringBuilder text)
    {
        msg.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = text.ToString()
        };
    }

    public static void SetBody(this MimeMessage msg, StringBuilder text, IAttachment[] attachments)
    {
        BodyBuilder builder = new();
        builder.HtmlBody = text.ToString();

        foreach(IAttachment attachment in attachments)
        {
            using(MemoryStream ms = new(attachment.Buffer))
            {
                MimePart part = new(attachment.ContentType)
                {
                    Content = new MimeContent(ms),
                    ContentDisposition = new(ContentDisposition.Attachment),
                    ContentTransferEncoding = ContentEncoding.Base64,
                    FileName = attachment.FileName
                };
                builder.Attachments.Add(part);
            }
        }

        msg.Body = builder.ToMessageBody();
    }
}
