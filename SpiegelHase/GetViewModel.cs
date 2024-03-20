using SpiegelHase.DataTransfer;

namespace SpiegelHase;

public class GetViewModel : BaseViewModel
{
    public string MessageString { get; set; }

    protected override void AddMessage(Message message)
    {
        base.AddMessage(message);
        MessageString = GetSerializedMessages();
    }

    public void Initialize()
    {
        MessageString = GetSerializedMessages();
    }
}
