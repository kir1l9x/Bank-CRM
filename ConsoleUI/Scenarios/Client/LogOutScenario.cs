using Services.Controllers;
using Spectre.Console;

namespace ConsoleUI.Scenarios.Client;

public class LogOutScenario : IScenario
{
    private readonly IClientController _clientController;

    public LogOutScenario(IClientController clientController)
    {
        _clientController = clientController;
    }

    public string Name => "Log Out";

    public void Run()
    {
        AnsiConsole.Clear();
        _clientController.LogOut();
    }
}