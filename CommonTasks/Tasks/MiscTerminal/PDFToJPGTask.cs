using PDFtoImage;
using SkiaSharp;
using System.Runtime.Versioning;
using TerminalWrapper;

namespace CommonTasks.Tasks.MiscTerminal;

[SupportedOSPlatform("windows")]
public sealed class PDFToJPGTask : MainTask
{
    public override string TaskName => "PDF to JPG";

    private const string m_filename = "CapacitacionMicrosoftTeams";

    public override async Task ExecuteAsync(CancellationToken cancelToken)
    {
        using (FileStream fs = File.OpenRead($"Resources/{m_filename}.pdf"))
        {
            SKBitmap[] pages = Conversion
                .ToImages(fs)
                .ToArray();

            int len = pages.Length;

            for (int i = 0; i < len; i++)
            {
                SKBitmap page = pages[i];
                SKData pageData = page.Encode(SKEncodedImageFormat.Jpeg, 32);
                byte[] pageBuffer = pageData.ToArray();

                int pageNumber = i + 1;
                int pageQuerSum = len
                    .ToString()
                    .Length;

                string pager = pageNumber
                    .ToString()
                    .PadLeft(pageQuerSum, '0');

                string directory = $"Output/{m_filename}";

                Directory.CreateDirectory(directory);

                await File.WriteAllBytesAsync($"{directory}/page_{pager}.jpg", pageBuffer, cancelToken);
            }
        }

        await Terminal.WriteAsync("Process ready");
    }
}
