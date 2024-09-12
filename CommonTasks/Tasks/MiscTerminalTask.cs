using CommonTasks.Tasks.MiscTerminal;
using System.Runtime.Versioning;
using TerminalWrapper;
using TerminalWrapper.CommonTasks;
using TerminalWrapper.Console;

namespace CommonTasks.Tasks;

[SupportedOSPlatform("windows")]
public sealed class MiscTerminalTask : MainTask
{
    public override string TaskName => "Open Misc Terminal";

    public override async Task ExecuteAsync(CancellationToken cancelToken)
    {
        MainTask[] tasks =
        [
            new GuidCreatorTask(),
            new PDFToJPGTask(),
            new ClearTerminalTask()
        ];

        ConsoleTerminalSettings terminalSettings = new()
        {
            CommandsMessage = "Choose a Misc task",
            PauseAfterExit = false,
            ValidateVersion = false,
            TerminalExitMessage = ""
        };

        Terminal terminal = ConsoleTerminal.CreateTerminal(tasks, terminalSettings);

        await terminal.RunAsync();
    }
}
