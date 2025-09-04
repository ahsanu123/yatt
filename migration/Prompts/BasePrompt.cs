using Spectre.Console;
using Spectre.Console.Rendering;
using YATT.Libs.Utilities;

namespace YATT.Migrations.Prompts;

public abstract class BasePrompt : IRunPrompt
{
    public void WriteLine(string text)
    {
        AnsiConsole.WriteLine(text);
    }
    public void DisplayPanel<T>(
        string headerText,
        List<T> datas,
        Func<T, Renderable> textTransformer
    )
    {
        var panelItems = new List<Renderable>();
        foreach (var data in datas)
        {
            panelItems.Add(textTransformer(data));
        }
        var panel = new Panel(new Rows(panelItems));
        panel.Header = new PanelHeader(headerText);

        AnsiConsole.Write(panel);
    }

    public T PromptChoicesToUser<T>(
        string title,
        T[] choices,
        Func<T, string> displayFunc,
        int pageSize = 5,
        string moreChoicesMessage = "[grey](Move up and down for more options, wrong into this menu?, just Ctrl+C)[/]"
    )
        where T : notnull
    {
        var selection = new SelectionPrompt<T>()
            .Title(title)
            .PageSize(pageSize)
            .AddChoices(choices)
            .MoreChoicesText(moreChoicesMessage);

        selection.Converter = displayFunc;

        return AnsiConsole.Prompt<T>(selection);
    }

    public PromptResult PromptWhatNext(List<PromptResult>? additionalMenu = null)
    {
        var listChoices = new List<PromptResult>();

        if (additionalMenu != null)
            listChoices.AddRange(additionalMenu);
        listChoices.AddRange(new[] { MainMenu.GetPromptResult(), PromptResult.Stop() });

        return PromptChoicesToUser<PromptResult>(
            title: "What Next!",
            listChoices.ToArray(),
            displayFunc: (result) => result.NextPrompt.Name.ToPascalCaseWithSpace()
        );
    }

    public abstract PromptResult Run();
}
