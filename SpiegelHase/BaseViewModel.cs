using Microsoft.AspNetCore.Mvc.ModelBinding;
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
        message.Content = message.Content
            .Replace("'", "&apos;");
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

    public void SetModelMessages(ModelStateDictionary modelState)
    {
        foreach (KeyValuePair<string, ModelStateEntry> kv in modelState)
        {
            if (kv.Value.ValidationState == ModelValidationState.Valid)
                continue;

            foreach (ModelError error in kv.Value.Errors)
                AddErrorMessage(error.ErrorMessage);
        }
    }

    public void TransferMessages(BaseViewModel originalModel)
    {
        foreach (Message ogMsg in originalModel.Messages)
        {
            foreach (Message rMsg in Messages)
                if (rMsg.Content == ogMsg.Content)
                    continue;

            Messages.Add(ogMsg);
        }
    }
}
