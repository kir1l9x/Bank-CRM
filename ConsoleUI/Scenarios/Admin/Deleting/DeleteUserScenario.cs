using Services.Controllers;
using Services.Controllers.OperationResults;
using Spectre.Console;

namespace ConsoleUI.Scenarios.Admin.Deleting;

public class DeleteUserScenario : IScenario
{
    private readonly IAdminController _adminController;

    public DeleteUserScenario(IAdminController adminController)
    {
        _adminController = adminController;
    }

    public string Name => "Delete user";

    public void Run()
    {
        string name = AnsiConsole.Ask<string>("Enter user name to delete: ");
        DeleteUserResult result = _adminController.DeleteUser(name);
        switch (result)
        {
            case DeleteUserResult.Success:
                AnsiConsole.MarkupLine("[green]Success[/]");
                break;
            case DeleteUserResult.Failure:
                AnsiConsole.MarkupLine("[red]User doesn't exist[/]");
                break;
        }
    }
}