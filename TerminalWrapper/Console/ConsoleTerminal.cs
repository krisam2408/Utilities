namespace TerminalWrapper.Console;

public sealed class ConsoleTerminal : Terminal
{
    private readonly int m_separatorLength;

    private ConsoleTerminal(int sepLength) : base() 
    {
        m_separatorLength = sepLength;
    }

    public static ConsoleTerminal CreateTerminal(MainTask[] tasks, int sepLength = 100)
    {
        ConsoleTerminal result = new(sepLength);

        int len = tasks.Length;
        result.ExitCommand = len + 1;
        result.Padding = result.ExitCommand.ToString().Length + 1;

        for(int i = 0; i < len; i++)
        {
            MainTask t = tasks[i];

            if (t != null)
            {
                t.Terminal = result;
                result.Tasks.Add(i + 1, t);
            }
        }

        result.OnExit += async () =>
        {
            await result.WriteAsync("Terminal closed...");
        };

        return result;
    }

    public override Task PauseAsync()
    {
        System.Console.ReadKey();
        return Task.CompletedTask;
    }

    public override async Task<string?> ReadAsync()
    {
        string? input = await System.Console.In.ReadLineAsync();
        return input;
    }

    public override async Task SeparatorAsync()
    {
        await WriteAsync(string.Empty.PadLeft(m_separatorLength, '-'));
    }

    public override async Task WriteAsync(string text)
    {
        await System.Console.Out.WriteLineAsync(text);
    }
}
