using Newtonsoft.Json;
using SpiegelHase.DataTransfer;

namespace SpiegelHase.Interfaces;

public abstract class BaseViewModel
{
    public List<Message> Messages { get; set; } = new();

    public void AddSuccessMessage(string message)
    {
        Messages.Add(new(message, "Success"));
    }

    public void AddErrorMessage(string message)
    {
        Messages.Add(new(message, "Error"));
    }

    public string SerializedMessages()
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
