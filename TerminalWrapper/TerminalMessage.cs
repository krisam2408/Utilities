namespace TerminalWrapper;

public class TerminalMessage
{
    public MessageLevel Level { get; private set; }
    public string Message { get; private set; }

    private TerminalMessage(string message, MessageLevel level)
    {
        Level = level;
        Message = message;
    }

    public static TerminalMessage Info(string message) => new(message, MessageLevel.Info);
    public static TerminalMessage Warning(string message) => new(message, MessageLevel.Warning);
    public static TerminalMessage Error(string message) => new(message, MessageLevel.Error);
}
