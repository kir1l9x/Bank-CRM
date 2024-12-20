using Entities.Users;
using Services.Controllers;
using Services.Controllers.OperationResults;
using Spectre.Console;

namespace ConsoleUI.Scenarios.Admin.Changing;

public class ChangeUserRoleScenario : IScenario
{
    private readonly IAdminController _adminController;

    public ChangeUserRoleScenario(IAdminController adminController)
    {
        _adminController = adminController;
    }

    public string Name => "Change user role";

    public void Run()
    {
        string curName = AnsiConsole.Ask<string>("Enter user to change role: ");
        string newRole = AnsiConsole.Ask<string>("Enter new user role: ");
        ChangingUserResult result;
        if (newRole.Equals("Admin", StringComparison.OrdinalIgnoreCase))
        {
            result = _adminController.ChangeUserRole(curName, new UserRole.Admin());
        }
        else if (newRole.Equals("Client", StringComparison.OrdinalIgnoreCase))
        {
            result = _adminController.ChangeUserRole(curName, new UserRole.Client());
        }
        else
        {
            throw new InvalidOperationException("Invalid user role");
        }

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