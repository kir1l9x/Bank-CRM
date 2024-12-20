using Services.Controllers;
using Services.Controllers.OperationResults;
using Spectre.Console;

namespace ConsoleUI.Scenarios.Admin.Changing;

public class ChangeUserNameScenario : IScenario
{
    private readonly IAdminController _adminController;

    public ChangeUserNameScenario(IAdminController adminController)
    {
        _adminController = adminController;
    }

    public string Name => "Change user name";

    public void Run()
    {
        string curName = AnsiConsole.Ask<string>("Enter current user name: ");
        string newName = AnsiConsole.Ask<string>("Enter new user name: ");
        ChangingUserResult result = _adminController.ChangeUserName(curName, newName);
        switch (result)
        {
            case ChangingUserResult.Success:
                AnsiConsole.MarkupLine("[green]Success rename![/]");
                break;
            case ChangingUserResult.Failure:
                AnsiConsole.MarkupLine("[red]Failure rename! User does not exist![/]");
                break;
        }
    }
}