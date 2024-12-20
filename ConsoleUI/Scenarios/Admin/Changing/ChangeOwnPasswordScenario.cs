using Services.Controllers;
using Spectre.Console;

namespace ConsoleUI.Scenarios.Admin.Changing;

public class ChangeOwnPasswordScenario : IScenario
{
    private readonly IAdminController _adminController;

    public ChangeOwnPasswordScenario(IAdminController adminController)
    {
        _adminController = adminController;
    }

    public string Name => "Change own password";

    public void Run()
    {
        string newPassword = AnsiConsole.Ask<string>("Enter your new password: ");
        _adminController.ChangeOwnAdminName(newPassword);
    }
}