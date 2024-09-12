using Newtonsoft.Json;

namespace TerminalWrapper;

public abstract class Terminal
{
    private CancellationTokenSource? m_cancelSource = null;

    protected Dictionary<int, MainTask> Tasks { get; private set; } = new();
    protected int Padding { get; set; }
    protected int ExitCommand { get; set; }

    public event Action? OnStart;
    public event Action? OnTaskCancel;
    public event Action? OnExit;

    public void InvokeTaskCancel() => OnTaskCancel?.Invoke();

    private readonly TerminalSettings m_settings;

    protected Terminal(TerminalSettings settings)
    {
        m_settings = settings;
    }

    public static T? GetConfiguration<T>() where T : TerminalSettings
    {
        TerminalSettings? appSettings = null;

        if (File.Exists("appsettings.json"))
        {
            string settings = File.ReadAllText("appsettings.json");
            appSettings = JsonConvert.DeserializeObject<T>(settings);
        }

        return (T?)appSettings;
    }

    public virtual async Task RunAsync()
    {
        List<Task> tasks = [ ExecutionAsync() ];

        await Task.WhenAll(tasks);
    }

    private void CancelInvoked()
    {
        if (m_cancelSource is null)
            return;

        m_cancelSource.Cancel();
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

    protected virtual async Task ExecutionAsync()
    {
        OnTaskCancel += CancelInvoked;

        OnStart?.Invoke();
        int currentInstruction = -1;

        m_settings.Validate();

        TerminalMessage[] messages = m_settings.GetMessages();

        foreach (TerminalMessage message in messages)
        {
            await WriteAsync($"{message.Level} - {message.Message}");
        }

        if (m_settings.HasErrors())
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

            if (currentInstruction > -1)
            {
                await SeparatorAsync();

                m_cancelSource = new();
                try
                {
                    await Tasks[currentInstruction].ExecuteAsync(m_cancelSource.Token);
                }
                catch (OperationCanceledException)
                {
                    await WriteAsync(m_settings.TaskCancellationMessage);
                }
                finally
                {
                    m_cancelSource.Dispose();
                    m_cancelSource = null;
                }
            }

        } while (currentInstruction >= -1);

        OnTaskCancel -= CancelInvoked;
        OnExit?.Invoke();

        if (m_cancelSource is not null)
        {
            m_cancelSource.Dispose();
            m_cancelSource = null;
        }
    }
}