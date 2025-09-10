using System.Reflection;

namespace YATT.Migrations.Configs;

public static class YattAssembly
{
    public static Assembly ExecutingAssembly => Assembly.GetExecutingAssembly();

    public static Stream? GetManifestResourceStream(string contentName) =>
        ExecutingAssembly.GetManifestResourceStream(contentName);
}
