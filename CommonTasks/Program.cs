using CommonTasks.Model;
using CommonTasks.Tasks;
using MorganaChains;
using Newtonsoft.Json;
using TerminalWrapper;
using TerminalWrapper.Console;

string configuration = await File.ReadAllTextAsync("appSettings.json");
AppSettings settings = JsonConvert.DeserializeObject<AppSettings>(configuration);

MainTask[] tasks =
[
    new GuidCreatorTask(),
    new GeneratePasswordTask(),
    new GenerateHashTask(HashFormat.MD5),
    new GenerateHashTask(HashFormat.SHA256),
    new GenerateHashTask(HashFormat.SHA384),
    new GenerateHashTask(HashFormat.SHA512),
    new EncryptStringTask(settings.AES.PublicKey, settings.AES.SecretKey),
    new DecryptStringTask(settings.AES.PublicKey, settings.AES.SecretKey),
];

ConsoleTerminal terminal = ConsoleTerminal.CreateTerminal(tasks);

await terminal.RunAsync();