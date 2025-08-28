using System.Text.Json;
using Xunit.Abstractions;

namespace YATT.Tests;

public static class TestOutputHelperExtension
{
    public static void SerializeObject(this ITestOutputHelper output, object obj)
    {
        var jsonString = JsonSerializer.Serialize(
            obj,
            new JsonSerializerOptions { WriteIndented = true }
        );
        output.WriteLine(jsonString);
    }
}
