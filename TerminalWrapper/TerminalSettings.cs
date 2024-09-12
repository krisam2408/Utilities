namespace TerminalWrapper;

public abstract class TerminalSettings
{
    public string Version { get; set; } = "1.1.1";
    public bool ValidateVersion { get; set; } = true;
    public string CommandsMessage { get; set; } = "Choose your task";
    public string ExitCommandMessage { get; set; } = "Exit";
    public string InvalidOptionMessage { get; set; } = "Not a valid option";
    public string TerminationMessage { get; set; } = "The terminal could not be executed due to errors";
    public string TaskCancellationMessage { get; set; } = "Task cancelled by user";
    public string VersionErrorMessage { get; set; } = "Application version could not be determined";

    private readonly List<TerminalMessage> m_messages = new();
    private bool m_hasInfo = false;
    private bool m_hasWarnings = false;
    private bool m_hasErrors = false;

    public virtual void Validate()
    {
        if (ValidateVersion && string.IsNullOrWhiteSpace(Version))
        {
            AddWarningMessage(VersionErrorMessage);
            Version = "0.0.1";
        }
    }

    public TerminalMessage[] GetMessages() => m_messages.ToArray();
    public void ClearMessages() => m_messages.Clear();
    public bool HasInfo() => m_hasInfo;
    public bool HasWarnings() => m_hasWarnings;
    public bool HasErrors() => m_hasErrors;

    public void AddInfoMessage(string message) 
    {
        m_messages.Add(TerminalMessage.Info(message));
        m_hasInfo = true;
    }

    public void AddWarningMessage(string message)
    {
        m_messages.Add(TerminalMessage.Warning(message));
        m_hasWarnings = true;
    }

    public void AddErrorMessage(string message)
    {
        m_messages.Add(TerminalMessage.Error(message));
        m_hasErrors = true;
    }
}
