using CommonTasks.Tasks;
using TerminalWrapper;
using TerminalWrapper.Console;

MainTask[] tasks =
[
    new GuidCreatorTask(),
];

ConsoleTerminal terminal = ConsoleTerminal.CreateTerminal(tasks);

await terminal.RunAsync();