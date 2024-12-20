using Services.Controllers;
using Services.Controllers.OperationResults;
using Spectre.Console;

namespace ConsoleUI.Scenarios.Admin.Show;

public class FindUserByUsernameScenario : IScenario
{
    private readonly IAdminController _adminController;

    public FindUserByUsernameScenario(IAdminController adminController)
    {
        _adminController = adminController;
    }

    public string Name => "Find User By Username";

    public void Run()
    {
        string username = AnsiConsole.Ask<string>("Enter User name:");
        FindingUserResult result = _adminController.FindUser(username);
        switch (result)
        {
            case FindingUserResult.Success:
                if (result.FoundUser is null)
                {
                    throw new Exception();
                }

                AnsiConsole.MarkupLine("[green]Success found![/]");
                AnsiConsole.MarkupLine(result.FoundUser.ToString());
                break;
            case FindingUserResult.Failed:
                AnsiConsole.MarkupLine("[red]User with this id was not found.[/]!");
                break;
        }
    }
}