using Spectre.Console;
using YATT.Migrations.ListMigration;

namespace YATT.Migrations.Prompts;

public class ListAvailableVersion : BasePrompt, IPromptResult
{
    public override PromptResult Run()
    {
        DisplayPanel(
            headerText: "List Available Version",
            datas: MigrationVersionList.ListVersion,
            textTransformer: (data) => new Text($"{data.Key} - {data.Value.Description}")
        );

        return PromptWhatNext(
            new List<PromptResult>
            {
                RunUpMigration.GetPromptResult(),
                RunDownMigration.GetPromptResult(),
            }
        );
    }

    public static PromptResult GetPromptResult() =>
        new PromptResult
        {
            NextAction = EnumPromptAction.GoNextPrompt,
            NextPrompt = typeof(ListAvailableVersion),
        };
}
