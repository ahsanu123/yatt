using Microsoft.Extensions.DependencyInjection;
using YATT.Migrations.Extensions;

var serviceCollection = new ServiceCollection();
serviceCollection.RegisterServices();

var serviceProvider = serviceCollection.BuildServiceProvider();

