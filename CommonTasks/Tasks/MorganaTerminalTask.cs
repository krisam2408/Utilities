using CommonTasks.Tasks.MorganaTerminal;
using MorganaChains;
using MorganaChains.DataTransfer;
using TerminalWrapper;
using TerminalWrapper.CommonTasks;
using TerminalWrapper.Console;

namespace CommonTasks.Tasks;

public sealed class MorganaTerminalTask : MainTask
{
    public override string TaskName => "Open Morgana Terminal";

    private readonly KeyPair m_settings;

    public MorganaTerminalTask(KeyPair settings)
    {
        m_settings = settings;
    }

    public override async Task ExecuteAsync(CancellationToken cancelToken)
    {
        MainTask[] tasks =
        [
            new GeneratePasswordTask(),
            new GenerateHashTask(HashFormat.MD5),
            new GenerateHashTask(HashFormat.SHA256),
            new GenerateHashTask(HashFormat.SHA384),
            new GenerateHashTask(HashFormat.SHA512),
            new EncryptStringTask(m_settings.PublicKey, m_settings.SecretKey),
            new DecryptStringTask(m_settings.PublicKey, m_settings.SecretKey),
            new ClearTerminalTask()
        ];

        ConsoleTerminalSettings terminalSettings = new()
        {
            CommandsMessage = "Choose a Morgana task",
            PauseAfterExit = false,
            ValidateVersion = false,
            TerminalExitMessage = ""
        };

        Terminal terminal = ConsoleTerminal.CreateTerminal(tasks, terminalSettings);

        await terminal.RunAsync();
    }
}
