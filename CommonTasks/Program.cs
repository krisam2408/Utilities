using CommonTasks.Tasks;
using MorganaChains;
using TerminalWrapper;
using TerminalWrapper.Console;

MainTask[] tasks =
[
    new GuidCreatorTask(),
    new GeneratePasswordTask(),
    new GenerateHashTask(HashFormat.MD5),
    new GenerateHashTask(HashFormat.SHA256),
    new GenerateHashTask(HashFormat.SHA384),
    new GenerateHashTask(HashFormat.SHA512),
];

ConsoleTerminal terminal = ConsoleTerminal.CreateTerminal(tasks);

await terminal.RunAsync();