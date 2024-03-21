using Newtonsoft.Json;
using SpiegelHase.DataAnnotations;
using SpiegelHase.DataTransfer;

namespace SpiegelHase;

public class GetViewModel : BaseViewModel
{
    [Ignorable] public string MessageString { get; set; }

    protected override void AddMessage(Message message)
    {
        base.AddMessage(message);
        MessageString = GetSerializedMessages();
    }

    public void Initialize()
    {
        if(!string.IsNullOrWhiteSpace(MessageString))
        {
            List<Message>? list = JsonConvert.DeserializeObject<List<Message>>(MessageString);
            if (list != null)
                Messages = list;
        }
    }
}
