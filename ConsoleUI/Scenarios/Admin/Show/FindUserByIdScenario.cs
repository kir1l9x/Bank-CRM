using Services.Controllers;
using Services.Controllers.OperationResults;
using Spectre.Console;

namespace ConsoleUI.Scenarios.Admin.Show;

public class FindUserByIdScenario : IScenario
{
    private readonly IAdminController _adminController;

    public FindUserByIdScenario(IAdminController adminController)
    {
        _adminController = adminController;
    }

    public string Name => "Find User ById";

    public void Run()
    {
        Guid userId = AnsiConsole.Ask<Guid>("Enter User ID: ");
        FindingUserResult result = _adminController.FindUser(userId);
        switch (result)
        {
            case FindingUserResult.Success:
                if (result.FoundUser is null)
                {
                    throw new Exception();
                }

                AnsiConsole.MarkupLine("[green]Success found![/]");
                AnsiConsole.Write(result.FoundUser.ToString());
                break;
            case FindingUserResult.Failed:
                AnsiConsole.MarkupLine("[red]User with this id was not found.[/]!");
                break;
        }
    }
}