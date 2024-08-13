namespace TerminalWrapper;

public abstract class TerminalSettings
{
    public string CommandsMessage { get; set; } = "Choose your test";
    public string ExitCommandMessage { get; set; } = "Exit";
    public string InvalidOptionMessage { get; set; } = "Not a valid option";
}
