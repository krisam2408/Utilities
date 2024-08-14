using Newtonsoft.Json;

namespace TerminalWrapper;

public abstract class Terminal
{
    protected Dictionary<int, MainTask> Tasks { get; private set; }
    protected int Padding { get; set; }
    protected int ExitCommand { get; set; }

    public event Action? OnStart;
    public event Action? OnExit;

    private readonly TerminalSettings m_settings;

    protected Terminal(TerminalSettings settings)
    {
        Tasks = new();
        m_settings = settings;
    }

    public static TerminalSettings? GetConfiguration<T>() where T : TerminalSettings
    {
        TerminalSettings? appSettings = null;

        if (File.Exists("appsettings.json"))
        {
            string settings = File.ReadAllText("appsettings.json");
            appSettings = JsonConvert.DeserializeObject<T>(settings);
        }

        return appSettings;
    }

    public virtual async Task RunAsync()
    {
        OnStart?.Invoke();
        int currentInstruction = -1;

        m_settings.Validate();

        foreach(TerminalMessage message in m_settings.Messages)
        {
            await WriteAsync($"{message.Level} - {message.Message}");
        }

        if (m_settings.HasErrors)
        {
            await WriteAsync(m_settings.TerminationMessage);
            return;
        }

        do
        {
            await SeparatorAsync();
            await WriteAsync(m_settings.CommandsMessage);
            string key;

            foreach (KeyValuePair<int, MainTask> kv in Tasks)
            {
                key = kv.Key.ToString().PadLeft(Padding);
                await WriteAsync($"  {key} -> {kv.Value.TaskName}");
            }

            key = ExitCommand.ToString().PadLeft(Padding);

            await WriteAsync($"  {key} -> {m_settings.ExitCommandMessage}");
            await SeparatorAsync();

            string? input = await ReadAsync();

            currentInstruction = await ValidateInstruction(input);
            await SeparatorAsync();

            if (currentInstruction > -1)
                await Tasks[currentInstruction].ExecuteAsync();

        } while (currentInstruction >= -1);

        OnExit?.Invoke();
    }

    private async Task<int> ValidateInstruction(string? input)
    {
        if(int.TryParse(input, out int result))
        {
            if (result == ExitCommand)
                return -2;

            if (Tasks.ContainsKey(result))
                return result;
        }

        await SeparatorAsync();
        await WriteAsync(m_settings.InvalidOptionMessage);

        return -1;
    }

    public abstract Task SeparatorAsync();
    
    public abstract Task SeparatorAsync(TerminalColor color);

    public abstract Task WriteAsync(string text);
    
    public abstract Task WriteAsync(string text, TerminalColor color);

    public abstract Task<string> ReadAsync();

    public abstract Task PauseAsync();

    public abstract Task ClearAsync();
}