using MorganaChains;
using TerminalWrapper;
using TextCopy;

namespace CommonTasks.Tasks;

public sealed class GenerateHashTask : MainTask
{
    private readonly HashFormat m_format;

    public override string TaskName => $"Generate Hash {m_format}";

    public GenerateHashTask(HashFormat format) : base()
    {
        m_format = format ; 
    }

    public override async Task ExecuteAsync()
    {
        await Terminal.WriteAsync("Write a phrase to hash:");
        string? phraseInput = await Terminal.ReadAsync();

        if (string.IsNullOrWhiteSpace(phraseInput))
        {
            await Terminal.WriteAsync("Input not recognized");
            return;
        }

        string result = Morgana.Hash(phraseInput, m_format);

        ClipboardService.SetText(result);

        await Terminal.WriteAsync($"Hash {m_format} \"{result}\" copied to Clipboard!");
    }
}
