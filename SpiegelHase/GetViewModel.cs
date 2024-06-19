using Newtonsoft.Json;
using SpiegelHase.DataAnnotations;
using SpiegelHase.DataTransfer;
using System.Text;

namespace SpiegelHase;

public class GetViewModel : BaseViewModel
{
    [Ignorable] public string MessageString { get; set; }

    protected override void AddMessage(Message message)
    {
        base.AddMessage(message);
        string messages = GetSerializedMessages();
        byte[] hashedMessages = Encoding.UTF8.GetBytes(messages);
        MessageString = Convert.ToBase64String(hashedMessages);
    }

    public void Initialize()
    {
        if(!string.IsNullOrWhiteSpace(MessageString))
        {
            byte[] hashedMessages = Convert.FromBase64String(MessageString);
            string messages = Encoding.UTF8.GetString(hashedMessages);
            List<Message>? list = JsonConvert.DeserializeObject<List<Message>>(messages);
            if (list != null)
                Messages = list;
        }
    }
}
