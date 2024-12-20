using Services.Controllers;
using Services.Controllers.OperationResults;
using Spectre.Console;

namespace ConsoleUI.Scenarios.Admin.Creation;

public class CreateNewUserScenario : IScenario
{
    private readonly IAdminController _adminController;

    public CreateNewUserScenario(IAdminController adminController)
    {
        _adminController = adminController;
    }

    public string Name => "Create new user";

    public void Run()
    {
        string name = AnsiConsole.Ask<string>("Enter new user name");
        string password = AnsiConsole.Ask<string>("Enter password for new user");
        CreationUserResult creationUserResult = _adminController.CreateNewUser(name, password);

        switch (creationUserResult)
        {
            case CreationUserResult.Success:
                AnsiConsole.MarkupLine("[green]User created successfully![/]");
                break;
            case CreationUserResult.UserWithThisNameExist:
                AnsiConsole.MarkupLine("[red]User with the name already exist![/]");
                break;
        }
    }
}