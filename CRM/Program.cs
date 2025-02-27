using ConsoleUI;
using ConsoleUI.Extensions;
using DataAccess.Extensions;
using Microsoft.Extensions.DependencyInjection;
using ServiceControllers.Extensions;

var collection = new ServiceCollection();

collection
    .AddControllers()
    .AddDataAccess(configuration =>
    {
        configuration.Host = "localhost";
        configuration.Port = 5432;
        configuration.Database = "postgres";
        configuration.Username = "kir1l9x";
        configuration.Password = "kitten";
        configuration.SslMode = "Prefer";
    })
    .AddConsoleScenarios();

ServiceProvider provider = collection.BuildServiceProvider();
using IServiceScope scope = provider.CreateScope();

ScenarioRunner scenarioRunner = scope.ServiceProvider.GetRequiredService<ScenarioRunner>();

while (true)
{
    scenarioRunner.Run();
}