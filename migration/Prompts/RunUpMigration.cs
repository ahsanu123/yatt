using FluentMigrator.Runner;
using YATT.Libs.Utilities;
using YATT.Migrations.ListMigration;

namespace YATT.Migrations.Prompts;

public class RunUpMigration : BasePrompt, IPromptResult
{
    private IMigrationRunner _migrationRunner;
    private IVersionLoader _versionLoader;

    public RunUpMigration(IMigrationRunner migrationRunner, IVersionLoader versionLoader)
    {
        _migrationRunner = migrationRunner;
        _versionLoader = versionLoader;
    }

    public override PromptResult Run()
    {
        WriteLine("Select Migration Up To Run!");

        var latestMigratedVersion = _versionLoader.VersionInfo.Latest();

        var selectedMigrationVersion = PromptChoicesToUser(
            title: "Choose Version",
            MigrationVersionList
                .ListVersion.Where(pr => pr.Value.Version > latestMigratedVersion)
                .ToArray(),
            displayFunc: (pair) => pair.Key.ToPascalCaseWithSpace()
        );

        WriteLine($"Wait Migrate Up To {selectedMigrationVersion.Key}...");

        _migrationRunner.MigrateUp(selectedMigrationVersion.Value.Version);

        latestMigratedVersion = _versionLoader.VersionInfo.Latest();

        if (latestMigratedVersion == selectedMigrationVersion.Value.Version)
            WriteLine($"Success To Migrate Up To {selectedMigrationVersion.Key}");

        return PromptWhatNext();
    }

    public static PromptResult GetPromptResult() =>
        new PromptResult
        {
            NextPrompt = typeof(RunUpMigration),
            NextAction = EnumPromptAction.GoNextPrompt,
        };
}
