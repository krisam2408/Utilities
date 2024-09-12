using TextCopy;
using TerminalWrapper;

namespace CommonTasks.Tasks.MiscTerminal;

internal sealed class GuidCreatorTask : MainTask
{
    public override string TaskName => "Create Guid";

    public override async Task ExecuteAsync(CancellationToken cancelToken)
    {
        string result = Guid
            .NewGuid()
            .ToString();

        ClipboardService.SetText(result);

        await Terminal.WriteAsync($"Guid \"{result}\" copied to Clipboard!");
    }
}
