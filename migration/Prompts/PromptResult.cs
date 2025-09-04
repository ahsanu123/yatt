namespace YATT.Migrations.Prompts;

public class PromptResult
{
    public EnumPromptAction NextAction { get; set; } = EnumPromptAction.Home;
    public Type NextPrompt { get; set; } = typeof(MainMenu);

    public static PromptResult Next(Type nextPrompt) =>
        new PromptResult { NextAction = EnumPromptAction.GoNextPrompt, NextPrompt = nextPrompt };

    public static PromptResult Stop() =>
        new PromptResult { NextAction = EnumPromptAction.Exit, NextPrompt = typeof(Exit) };

    public static PromptResult Home() =>
        new PromptResult
        {
            NextAction = EnumPromptAction.GoNextPrompt,
            NextPrompt = typeof(MainMenu),
        };
}
