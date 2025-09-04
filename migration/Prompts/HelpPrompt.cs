namespace YATT.Migrations.Prompts;

public class Help : BasePrompt, IPromptResult
{
    public override PromptResult Run()
    {
        string helpText = """
            TODO, create real Help Text.

            yatt-migration is a database migration utility designed to manage and
            apply schema changes across multiple relational database engines. It
            enables developers and operators to create, version, and execute 
            database migrations in a consistent and repeatable manner, ensuring 
            that application code and database schemas evolve together.

            This tool provides a flexible migration framework that works with
            PostgreSQL, SQL Server, SQLite, and other providers through an
            extensible runner system. Migrations can be authored in C# or SQL and
            organized into ordered steps that the tool applies sequentially. 

            The application supports both forward and backward migrations, making
            it easy to upgrade or rollback schema changes safely. It integrates
            well into CI/CD pipelines and can be invoked manually or scripted
            as part of automated deployment workflows.

            Configuration is handled through standard .NET options and JSON-based
            settings files. Sensitive data such as connection strings can be
            securely managed using environment variables or secrets providers.
            Logging and diagnostics are included to assist in tracking migration
            progress and troubleshooting failures.

            yatt-migration also includes utility features such as generating blank
            migration templates, displaying the migration history applied to a
            given database, and validating the consistency of migrations across
            multiple environments. By consolidating schema changes into a single
            workflow, it reduces human error and improves team collaboration.

            Typical use cases include:
                • Applying new schema changes during feature deployment
                • Rolling back failed database updates
                • Validating schema consistency across environments
                • Automating migrations in build and release pipelines
                • Keeping developer and production databases in sync

            yatt-migration also includes utility features such as generating blank
            migration templates, displaying the migration history applied to a
            given database, and validating the consistency of migrations across
            multiple environments. By consolidating schema changes into a single
            workflow, it reduces human error and improves team collaboration.

            """;
        WriteLine(helpText);

        return PromptWhatNext();
    }

    public static PromptResult GetPromptResult() =>
        new PromptResult { NextPrompt = typeof(Help), NextAction = EnumPromptAction.GoNextPrompt };
}
