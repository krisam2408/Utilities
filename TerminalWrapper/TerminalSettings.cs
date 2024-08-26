namespace TerminalWrapper;

public abstract class TerminalSettings
{
    public string CommandsMessage { get; set; } = "Choose your test";
    public string ExitCommandMessage { get; set; } = "Exit";
    public string InvalidOptionMessage { get; set; } = "Not a valid option";
    public string TerminationMessage { get; set; } = "The terminal could not be executed due to errors";
    public string TaskCancellationMessage { get; set; } = "Task cancelled by user";

    public List<TerminalMessage> Messages { get; private set; } = new();
    public bool HasInfo { get; private set; } = false;
    public bool HasWarnings { get; private set; } = false;
    public bool HasErrors { get; private set; } = false;

    public abstract void Validate();

    public void AddInfoMessage(string message) 
    {
        Messages.Add(TerminalMessage.Info(message));
        HasInfo = true;
    }

    public void AddWarningMessage(string message)
    {
        Messages.Add(TerminalMessage.Warning(message));
        HasWarnings = true;
    }

    public void AddErrorMessage(string message)
    {
        Messages.Add(TerminalMessage.Error(message));
        HasErrors = true;
    }
}
