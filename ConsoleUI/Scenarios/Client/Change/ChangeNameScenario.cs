using Services.Controllers;
using Spectre.Console;

namespace ConsoleUI.Scenarios.Client.Change;

public class ChangeNameScenario : IScenario
{
    private readonly IClientController _clientController;

    public ChangeNameScenario(IClientController clientController)
    {
        _clientController = clientController;
    }

    public string Name => "Change name";

    public void Run()
    {
        string newName = AnsiConsole.Ask<string>("Enter new name: ");
        _clientController.ChangeName(newName);
    }
}