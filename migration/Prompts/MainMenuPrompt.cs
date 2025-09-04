using Spectre.Console;

namespace YATT.Migrations.Prompts;

public class MainMenu : BasePrompt, IPromptResult
{
    public override PromptResult Run()
    {
        AnsiConsole.Write(new FigletText("YATT - Migration").LeftJustified().Color(Color.Red));

        return PromptWhatNext(
            new List<PromptResult> { ListAvailableVersion.GetPromptResult(), ListMigrated.GetPromptResult() }
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
