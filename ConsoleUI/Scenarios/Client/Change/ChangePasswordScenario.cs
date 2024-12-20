using Services.Controllers;
using Spectre.Console;

namespace ConsoleUI.Scenarios.Client.Change;

public class ChangePasswordScenario : IScenario
{
    private readonly IClientController _clientController;

    public ChangePasswordScenario(IClientController clientController)
    {
        _clientController = clientController;
    }

    public string Name => "Change password";

    public void Run()
    {
        string newPassword = AnsiConsole.Ask<string>("Enter new password: ");
        _clientController.ChangePassword(newPassword);
    }
}