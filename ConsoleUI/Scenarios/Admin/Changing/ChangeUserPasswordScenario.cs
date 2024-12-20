using Services.Controllers;
using Services.Controllers.OperationResults;
using Spectre.Console;

namespace ConsoleUI.Scenarios.Admin.Changing;

public class ChangeUserPasswordScenario : IScenario
{
    private readonly IAdminController _adminController;

    public ChangeUserPasswordScenario(IAdminController adminController)
    {
        _adminController = adminController;
    }

    public string Name => "Change user password";

    public void Run()
    {
        string curName = AnsiConsole.Ask<string>("Enter user to change name: ");
        string newPassword = AnsiConsole.Ask<string>("Enter new user password: ");
        ChangingUserResult result = _adminController.ChangeUserPassword(curName, newPassword);
        switch (result)
        {
            case ChangingUserResult.Success:
                AnsiConsole.MarkupLine("[green]Success edit![/]");
                break;
            case ChangingUserResult.Failure:
                AnsiConsole.MarkupLine("[red]Failure edit! User does not exist![/]");
                break;
        }
    }
}