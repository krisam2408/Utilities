using MorganaChains;
using MorganaChains.Settings;
using TerminalWrapper;
using TextCopy;

namespace CommonTasks.Tasks.MorganaTerminal;

public sealed class EncryptStringTask : MainTask
{
    private readonly string m_publicKey;
    private readonly string m_secretKey;

    public override string TaskName => "Encrypt String";

    public EncryptStringTask(string publicKey, string secretKey) : base()
    {
        m_publicKey = publicKey;
        m_secretKey = secretKey;
    }

    public override async Task ExecuteAsync(CancellationToken cancelToken)
    {
        await Terminal.WriteAsync("Write a string to encrypt:");
        string? phraseInput = await Terminal.ReadAsync();

        if (string.IsNullOrWhiteSpace(phraseInput))
        {
            await Terminal.WriteAsync("Input not recognized");
            return;
        }

        AESMorganaSettings morganaSettings = new(m_publicKey, m_secretKey);
        AESMorgana morgana = new(morganaSettings);

        string result = morgana.Encrypt(phraseInput);

        ClipboardService.SetText(result);

        await Terminal.WriteAsync($"Encrypted result \"{result}\" copied to Clipboard!");
    }
}
