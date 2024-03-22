using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using SpiegelHase.DataTransfer;
using System.ComponentModel;
using System.Reflection;

namespace SpiegelHase;

public class BaseViewModel
{
    private static readonly Dictionary<string, string> ErrorTranslation = new()
    {
        { "0", "Ha ocurrido un error" },
        { "field is required", "El campo &apos;{0}&apos; es requerido" },
        { "e-mail address", "El campo &apos;{0}&apos; tiene que ser un correo electrónico válido" },
        { "rut format", "El campo &apos;{0}&apos; no es un Rut válido" }
    };
    
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

    private static string HandleMessage(string errorMessage)
    {
        foreach (KeyValuePair<string, string> kv in ErrorTranslation)
            if (errorMessage.Contains(kv.Key))
                return kv.Value;
        return ErrorTranslation["0"];
    }

    public void SetModelMessages(ModelStateDictionary modelState)
    {
        foreach (KeyValuePair<string, ModelStateEntry> kv in modelState)
        {
            if (kv.Value.ValidationState == ModelValidationState.Valid)
                continue;

            string display = GetPropertyDisplay(kv.Key);

            foreach (ModelError error in kv.Value.Errors)
            {
                string message = HandleMessage(error.ErrorMessage);
                string result = string.Format(message, display);
                AddErrorMessage(result);
            }
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

    private string GetPropertyDisplay(string propertyName)
    {
        PropertyInfo? key = GetType()
            .GetProperty(propertyName);

        if (key == null)
            return propertyName;

        DisplayNameAttribute? attr = key.GetCustomAttribute<DisplayNameAttribute>();

        if (attr == null)
            return propertyName;

        return attr.DisplayName;
    }
}
