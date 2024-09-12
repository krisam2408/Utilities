using CommonTasks.Model;
using CommonTasks.Tasks;
using System.Runtime.Versioning;
using TerminalWrapper;
using TerminalWrapper.CommonTasks;
using TerminalWrapper.Console;

namespace CommonTasks;

[SupportedOSPlatform("windows")]
public static class Program
{
    public static async Task Main()
    {
        AppSettings? settings = Terminal.GetConfiguration<AppSettings>();

        if (settings is null)
        {
            await Console.Out.WriteLineAsync("App configuration could not be set and will be terminated");
            await Console.Out.WriteLineAsync("Press any key to close this window");
            Console.ReadKey();
            return;
        }

        MainTask[] tasks =
        [
            new MiscTerminalTask(),
            new MorganaTerminalTask(settings.AES),
            new ClearTerminalTask()
        ];

        Terminal terminal = ConsoleTerminal.CreateTerminal(tasks, settings);

        terminal.OnStart += async () =>
        {
            await terminal.SeparatorAsync();
            await terminal.WriteAsync("Common Tasks");
            await terminal.WriteAsync($"Version {settings.Version}");
        };

        await terminal.RunAsync();
    }
}