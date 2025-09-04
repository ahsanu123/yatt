using FluentMigrator.Runner;
using Spectre.Console;
using YATT.Migrations.ListMigration;

namespace YATT.Migrations.Prompts;

public class ListMigrated : BasePrompt, IPromptResult
{
    private IMigrationRunner _migrationRunner;
    private IVersionLoader _versionLoader;

    public ListMigrated(IMigrationRunner migrationRunner, IVersionLoader versionLoader)
    {
        _migrationRunner = migrationRunner;
        _versionLoader = versionLoader;
    }

    public override PromptResult Run()
    {
        var listMigratedVersion = _versionLoader
            .VersionInfo.AppliedMigrations()
            .Select(version =>
                MigrationVersionList
                    .ListVersion.Where(pr => pr.Value.Version == version)
                    .FirstOrDefault()
            )
            .ToList();

        if (listMigratedVersion.Count() > 0)
        {
            DisplayPanel(
                headerText: "List Migrated Version",
                datas: listMigratedVersion,
                textTransformer: (data) => new Text($"{data.Key} - {data.Value.Description}")
            );
        }
        else
            AnsiConsole.WriteLine("No Migrated Version.");

        return PromptWhatNext();
    }

    public static PromptResult GetPromptResult() =>
        new PromptResult
        {
            NextAction = EnumPromptAction.GoNextPrompt,
            NextPrompt = typeof(ListMigrated),
        };
}
