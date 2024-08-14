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

    public override async Task SeparatorAsync(TerminalColor color)
    {
        ConsoleColor targetColor = TranslateColor(color);
        ConsoleColor currentColor = System.Console.ForegroundColor;
        System.Console.ForegroundColor = targetColor;

        await SeparatorAsync();

        System.Console.ForegroundColor = currentColor;
    }

    public override async Task WriteAsync(string text)
    {
        await System.Console.Out.WriteLineAsync(text);
    }

    public override async Task WriteAsync(string text, TerminalColor color)
    {
        ConsoleColor targetColor = TranslateColor(color);
        ConsoleColor currentColor = System.Console.ForegroundColor;
        System.Console.ForegroundColor = targetColor;

        await WriteAsync(text);

        System.Console.ForegroundColor = currentColor;
    }

    public override async Task ClearAsync()
    {
        System.Console.Clear();

        await Task.Delay(1);
    }

    private static ConsoleColor TranslateColor(TerminalColor color)
    {
        return color.Name switch
        {
            "Black" => ConsoleColor.Black,
            "Red" => ConsoleColor.Red,
            "Yellow" => ConsoleColor.Yellow,
            "Green" => ConsoleColor.Green,
            "Cyan" => ConsoleColor.Cyan,
            "Blue" => ConsoleColor.Blue,
            "Magenta" => ConsoleColor.Magenta,
            _ => ConsoleColor.White,
        };
    }
}