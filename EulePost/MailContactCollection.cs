using MimeKit;

namespace EulePost;

public sealed class MailContactCollection
{
    private readonly List<MailContact> m_contacts = new();

    public bool TryAddContact(MailContact contact)
    {
        if(contact.Type == MailContact.ContactType.From)
        {
            if(CountByType(MailContact.ContactType.From) == 0)
            {
                m_contacts.Add(contact);
                return true;
            }
            return false;
        }

        if (Contains(contact.Email))
            return false;

        m_contacts.Add(contact);
        return true;
    }

    public bool Contains(string email)
    {
        foreach(MailContact contact in m_contacts)
            if (contact.Email == email)
                return true;
        return false;
    }

    private int CountByType(MailContact.ContactType type)
    {
        List<MailContact> result = new();

        foreach (MailContact contact in m_contacts)
            if(contact.Type == type)
                result.Add(contact);

        return result.Count;
    }

    public bool IsValid()
    {
        bool from = CountByType(MailContact.ContactType.From) == 1;
        bool to = CountByType(MailContact.ContactType.To) > 0;

        return from && to;
    }

    public MailContact[] ToList()
    {
        return m_contacts.ToArray();
    }
}

public struct MailContact
{
    public string Display { get; set; }
    public string Email { get; set; }
    public ContactType Type { get; set; }

    public MailContact(string displayName, string email, ContactType type)
    {
        Display = displayName;
        Email = email;
        Type = type;
    }

    public MailboxAddress ToAddress()
    {
        if(string.IsNullOrWhiteSpace(Display))
            return MailboxAddress.Parse(Email);
        return new(Display, Email);
    }

    public enum ContactType
    {
        From,
        To,
        CC,
        BCC
    }
}
