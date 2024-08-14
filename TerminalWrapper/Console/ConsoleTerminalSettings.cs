namespace TerminalWrapper.Console;

public class ConsoleTerminalSettings : TerminalSettings
{
    public int SeparatorLength { get; set; } = 96;
    public string TerminalExitMessage { get; set; } = "Terminal closed...";
    public int TerminalWidth { get; set; } = 720;
    public int TerminalHeight { get; set; } = 405;

    public override void Validate()
    {
        
    }
}
