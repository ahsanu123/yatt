using Microsoft.Extensions.DependencyInjection;
using YATT.Migrations.Extensions;
using YATT.Migrations.Prompts;

Console.Clear();
Console.WriteLine("Loading...");

var serviceCollection = new ServiceCollection();
serviceCollection.RegisterServices();

var serviceProvider = serviceCollection.BuildServiceProvider();

serviceProvider.RunPrompts(typeof(MainMenu));

