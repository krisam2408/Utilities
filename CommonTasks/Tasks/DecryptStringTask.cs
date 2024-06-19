using MorganaChains;
using TerminalWrapper;
using TextCopy;

namespace CommonTasks.Tasks;

public sealed class DecryptStringTask : MainTask
{
    private readonly string m_publicKey;
    private readonly string m_secretKey;

    public override string TaskName => "Decrypt String";

    public DecryptStringTask(string publicKey, string secretKey) : base()
    {
        m_publicKey = publicKey;
        m_secretKey = secretKey;
    }

    public override async Task ExecuteAsync()
    {
        await Terminal.WriteAsync("Write a string to decrypt:");
        string? phraseInput = await Terminal.ReadAsync();

        if (string.IsNullOrWhiteSpace(phraseInput))
        {
            await Terminal.WriteAsync("Input not recognized");
            return;
        }

        AESMorganaSettings morganaSettings = new(m_publicKey, m_secretKey);
        AESMorgana morgana = new(morganaSettings);

        string? result = morgana.Decrypt(phraseInput);

        if (string.IsNullOrWhiteSpace(result))
        {
            await Terminal.WriteAsync("Not possible to decrypt");
            return;
        }

        ClipboardService.SetText(result);

        await Terminal.WriteAsync($"Decrypted result \"{result}\" copied to Clipboard!");
    }
}
