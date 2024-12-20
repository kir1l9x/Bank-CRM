using Services.Controllers;
using Spectre.Console;

namespace ConsoleUI.Scenarios.Admin.Changing;

public class ChangeOwnNameScenario : IScenario
{
    private readonly IAdminController _adminController;

    public ChangeOwnNameScenario(IAdminController adminController)
    {
        _adminController = adminController;
    }

    public string Name => "Change own name";

    public void Run()
    {
        string newName = AnsiConsole.Ask<string>("Enter your new name: ");
        _adminController.ChangeOwnAdminName(newName);
    }
}