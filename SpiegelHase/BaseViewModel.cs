using Newtonsoft.Json;
using SpiegelHase.DataTransfer;

namespace SpiegelHase;

public class BaseViewModel
{
    public List<Message> Messages { get; set; } = new();

    public void AddSuccessMessage(string message)
    {
        AddMessage(new(message, "Success"));
    }

    public void AddInfoMessage(string message)
    {
        AddMessage(new(message, "Info"));
    }

    public void AddWarningMessage(string message)
    {
        AddMessage(new(message, "Warning"));
    }

    public void AddErrorMessage(string message)
    {
        AddMessage(new(message, "Error"));
    }

    protected virtual void AddMessage(Message message)
    {
        Messages.Add(message);
    }

    public string GetSerializedMessages()
    {
        return JsonConvert.SerializeObject(Messages);
    }

    public bool HasInterface(Type interfaceType)
    {
        Type type = GetType();
        Type[] interfaces = type.GetInterfaces();
        return interfaces.Contains(interfaceType);
    }
}
