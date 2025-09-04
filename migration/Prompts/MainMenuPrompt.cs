using Microsoft.Extensions.Options;
using Spectre.Console;
using YATT.Migrations.Configs;

namespace YATT.Migrations.Prompts;

public class MainMenu : BasePrompt, IPromptResult
{
    private YattConfig _yattConfig;

    public MainMenu(IOptions<YattConfig> yattConfig)
    {
        _yattConfig = yattConfig.Value;
    }

    public override PromptResult Run()
    {
        var figleFontStream = YattAssembly.ExecutingAssembly.GetManifestResourceStream(
            EmbeddedContentList.SlantFigleFont
        );
        if (figleFontStream != null)
        {
            var figletFont = FigletFont.Load(figleFontStream);

            AnsiConsole.Write(
                new FigletText(figletFont, "YATT - Migration").LeftJustified().Color(Color.Red)
            );
        }
        else
            AnsiConsole.Write(new FigletText("YATT - Migration").LeftJustified().Color(Color.Red));

        return PromptWhatNext(
            new List<PromptResult>
            {
                ListAvailableVersion.GetPromptResult(),
                ListMigrated.GetPromptResult(),
                Help.GetPromptResult()
            }
        );
    }

    public static PromptResult GetPromptResult() =>
        new PromptResult
        {
            NextPrompt = typeof(MainMenu),
            NextAction = EnumPromptAction.GoNextPrompt,
        };

    public static PromptResult StopPromptResult() =>
        new PromptResult { NextAction = EnumPromptAction.Exit };
}
