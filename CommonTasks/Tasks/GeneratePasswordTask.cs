using MorganaChains;
using TerminalWrapper;
using TextCopy;

namespace CommonTasks.Tasks
{
    public sealed class GeneratePasswordTask : MainTask
    {
        public override string TaskName => "Generate Password";

        public override async Task ExecuteAsync()
        {
            await Terminal.WriteAsync("Define password length:");
            string? lengthInput = await Terminal.ReadAsync();

            if(int.TryParse(lengthInput, out int length))
            {
                Morgana morgana = new();
                string result = morgana.GeneratePasswordOfLength(length);

                ClipboardService.SetText(result);

                await Terminal.WriteAsync($"Password \"{result}\" copied to Clipboard!");

                return;
            }

            await Terminal.WriteAsync("Input not recognized");

        }
    }
}
