using Services.Controllers;
using Spectre.Console;

namespace ConsoleUI.Scenarios.Admin;

public class LogOutScenario : IScenario
{
    private readonly IAdminController _adminController;

    public LogOutScenario(IAdminController adminController)
    {
        _adminController = adminController;
    }

    public string Name => "Log Out";

    public void Run()
    {
        AnsiConsole.Clear();
        _adminController.LogOut();
    }
}