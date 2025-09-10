using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using YATT.Migrations.Prompts;

namespace YATT.Migrations.Extensions;

public static class PromptsExtension
{
    public static IServiceCollection AddPromptServices(this IServiceCollection services)
    {
        services.AddSingleton<MainMenu>();
        services.AddSingleton<ListMigrated>();
        services.AddSingleton<ListAvailableVersion>();
        services.AddSingleton<Exit>();
        services.AddSingleton<RunUpMigration>();
        services.AddSingleton<RunDownMigration>();
        services.AddSingleton<Help>();

        return services;
    }

    public static void RunPrompts(this IServiceProvider service, Type initialPrompt)
    {
        var entryPrompt = service.GetService(initialPrompt) as IRunPrompt;
        if (entryPrompt == null)
        {
            AnsiConsole.WriteLine($"Cant Find Prompt Class: {initialPrompt.FullName}");
            return;
        }

        Console.Clear();
        var nextPrompt = entryPrompt!.Run();

        while (nextPrompt.NextAction != EnumPromptAction.Exit)
        {

            try
            {
                Console.Clear();
                var prompt = service.GetService(nextPrompt.NextPrompt) as IRunPrompt;

                if (prompt == null)
                {
                    AnsiConsole.WriteLine($"Cant Find Prompt Class: {nextPrompt.NextPrompt.FullName}");
                    break;
                }

                nextPrompt = prompt.Run();
            }
            catch (System.Exception e)
            {
                nextPrompt = MainMenu.GetPromptResult();

                Console.Clear();

                AnsiConsole.WriteLine("Ooops..., Somethink Error.");
                AnsiConsole.WriteLine("---");

                AnsiConsole.WriteException(e);

                AnsiConsole.WriteLine("---");
                AnsiConsole.WriteLine("Enter To Go Main Menu");
                Console.ReadLine();
            }
        }

        AnsiConsole.WriteLine("Goodbye...");
    }

    public static PromptResult PromptResult(this IRunPrompt runPrompt) =>
        new PromptResult
        {
            NextAction = EnumPromptAction.GoNextPrompt,
            NextPrompt = runPrompt.GetType(),
        };
}
