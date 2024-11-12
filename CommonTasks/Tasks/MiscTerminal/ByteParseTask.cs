using System.Text;
using TerminalWrapper;

namespace CommonTasks.Tasks.MiscTerminal;

internal sealed class ByteParseTask : MainTask
{
    public override string TaskName => "Parse Text to Bytes";

    public override async Task ExecuteAsync(CancellationToken cancelToken)
    {
        await Terminal.WriteAsync("Write a text");
        string? input = await Terminal.ReadAsync();

        if (string.IsNullOrWhiteSpace(input))
        {
            await Terminal.WriteAsync("Invalid input");
            return;
        }

        await Terminal.SeparatorAsync();

        byte[] chunk = Encoding.UTF8.GetBytes(input);

        string parse = Aide.Byte.TextParse(chunk);

        await Terminal.WriteAsync(parse);

        if (parse.Last() != '\n')
            await Terminal.WriteAsync("");

        await Terminal.WriteAsync($"Byte Size: {chunk.Length * 8}");
    }
}
