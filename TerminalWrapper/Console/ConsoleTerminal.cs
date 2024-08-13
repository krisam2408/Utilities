namespace TerminalWrapper.Console;

public sealed class ConsoleTerminal : Terminal
{
    private readonly ConsoleTerminalSettings m_settings;

    private ConsoleTerminal(ConsoleTerminalSettings settings) : base(settings) 
    {
        m_settings = settings;
    }

    public static ConsoleTerminal CreateTerminal(IEnumerable<MainTask> tasks, ConsoleTerminalSettings configuration)
    {
        ConsoleTerminal result = new(configuration);

        MainTask[] taskArray = tasks.ToArray();

        int len = taskArray.Length;
        result.ExitCommand = len + 1;
        result.Padding = result.ExitCommand.ToString().Length + 1;

        for (int i = 0; i < len; i++)
        {
            MainTask t = taskArray[i];

            if (t is not null)
            {
                t.Terminal = result;
                result.Tasks.Add(i + 1, t);
            }
        }

        result.OnStart += () =>
        {
            System.Console.SetWindowSize(configuration.TerminalWidth, configuration.TerminalHeight);
        };

        result.OnExit += async () =>
        {
            await result.WriteAsync(result.m_settings.TerminalExitMessage);
        };

        return result;
    }

    public static ConsoleTerminal CreateTerminal(IEnumerable<MainTask> tasks)
    {
        ConsoleTerminalSettings settings = new();

        return CreateTerminal(tasks, settings);
    }

    public override Task PauseAsync()
    {
        System.Console.ReadKey();
        return Task.CompletedTask;
    }

    public override async Task<string> ReadAsync()
    {
        string? input = await System.Console.In.ReadLineAsync();

        if(string.IsNullOrWhiteSpace(input))
            return "";

        return input;
    }

    public override async Task SeparatorAsync()
    {
        await WriteAsync(string.Empty.PadLeft(m_settings.SeparatorLength, '-'));
    }

    public override async Task WriteAsync(string text)
    {
        await System.Console.Out.WriteLineAsync(text);
    }

    public override Task ClearAsync()
    {
        System.Console.Clear();
        return Task.CompletedTask;
    }
}