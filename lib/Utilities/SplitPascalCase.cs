using System.Text.RegularExpressions;

namespace YATT.Libs.Utilities;

public static class SplitPascalCase
{
    public static string ToPascalCaseWithSpace(this string text)
    {
        return Regex.Replace(text, "(\\B[A-Z])", " $1");
    }
}
