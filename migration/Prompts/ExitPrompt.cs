using YATT.Libs.Utilities;

namespace YATT.Migrations.Prompts;

public class Exit : BasePrompt, IPromptResult
{
    public override PromptResult Run()
    {
        var nextPrompt = PromptChoicesToUser<PromptResult>(
            title: "Migration Menu",
            new[] { ListAvailableVersion.GetPromptResult(), ListMigrated.GetPromptResult() },
            displayFunc: (result) => result.NextPrompt.Name.ToPascalCaseWithSpace()
        );

        return nextPrompt;
    }

    public static PromptResult GetPromptResult() =>
        new PromptResult { NextPrompt = typeof(Exit), NextAction = EnumPromptAction.Exit };
}
