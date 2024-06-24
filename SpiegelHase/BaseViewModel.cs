using Microsoft.AspNetCore.Mvc.ModelBinding;
using SpiegelHase.DataTransfer;
using SpiegelHase.Handlers;

namespace SpiegelHase;

public class BaseViewModel
{
    public MessageHandler? MessageHandler { get; set; } = new();

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
        MessageHandler?.Add(message);
    }

    public string GetSerializedMessages()
    {
        if(MessageHandler == null)
            MessageHandler = new();

        return MessageHandler.Serialize();
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

    public virtual void TransferMessages(BaseViewModel originalModel)
    {
        if(MessageHandler == null)
            MessageHandler = new();

        MessageHandler.TransferMessages(originalModel.MessageHandler);
    }
}
