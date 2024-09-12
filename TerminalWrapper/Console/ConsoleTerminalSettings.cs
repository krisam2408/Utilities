namespace TerminalWrapper.Console;

public class ConsoleTerminalSettings : TerminalSettings
{
    public int SeparatorLength { get; set; } = 96;
    public string TerminalExitMessage { get; set; } = "Terminal closed...";
    public bool PauseAfterExit { get; set; } = true;
}
