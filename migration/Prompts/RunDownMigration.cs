using FluentMigrator.Runner;
using YATT.Libs.Utilities;
using YATT.Migrations.ListMigration;

namespace YATT.Migrations.Prompts;

public class RunDownMigration : BasePrompt, IPromptResult
{
    private IMigrationRunner _migrationRunner;
    private IVersionLoader _versionLoader;

    public RunDownMigration(IMigrationRunner migrationRunner, IVersionLoader versionLoader)
    {
        _migrationRunner = migrationRunner;
        _versionLoader = versionLoader;
    }

    public override PromptResult Run()
    {
        var latestMigratedVersion = _versionLoader.VersionInfo.Latest();
        var listDownMigration = MigrationVersionList
            .ListVersion.Where(pr => pr.Value.Version < latestMigratedVersion)
            .ToArray();

        if (listDownMigration.Count() > 0)
        {
            WriteLine("Select Migration Down To Run!");

            var selectedMigrationVersion = PromptChoicesToUser(
                title: "Choose Version",
                choices: listDownMigration,
                displayFunc: (pair) => pair.Key.ToPascalCaseWithSpace()
            );

            WriteLine($"Wait Migrate Down To {selectedMigrationVersion.Key}...");

            _migrationRunner.MigrateDown(selectedMigrationVersion.Value.Version);

            latestMigratedVersion = _versionLoader.VersionInfo.Latest();

            if (latestMigratedVersion == selectedMigrationVersion.Value.Version)
                WriteLine($"Success To Migrate Down To {selectedMigrationVersion.Key}");
        }
        else
            WriteLine("Database Still Empty, cant do Down Migration");

        return PromptWhatNext();
    }

    public static PromptResult GetPromptResult() =>
        new PromptResult
        {
            NextPrompt = typeof(RunDownMigration),
            NextAction = EnumPromptAction.GoNextPrompt,
        };
}
