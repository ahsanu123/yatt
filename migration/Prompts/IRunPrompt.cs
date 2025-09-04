namespace YATT.Migrations.Prompts;

public enum EnumPromptAction
{
    Back,
    Home,
    GoNextPrompt,
    Exit,
}

public interface IRunPrompt
{
    // return Type to run another prompt
    // or null to terminate prompt
    public PromptResult Run();
}
