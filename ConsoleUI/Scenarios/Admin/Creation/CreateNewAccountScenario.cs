using Services.Controllers;
using Services.Controllers.OperationResults;
using Spectre.Console;

namespace ConsoleUI.Scenarios.Admin.Creation;

public class CreateNewAccountScenario : IScenario
{
    private readonly IAdminController _adminController;

    public CreateNewAccountScenario(IAdminController adminController)
    {
        _adminController = adminController;
    }

    public string Name => "Create new account";

    public void Run()
    {
        string name = AnsiConsole.Ask<string>("Enter user name account for");
        CreationAccountResult creationAccountResult = _adminController.CreateNewAccount(name);

        switch (creationAccountResult)
        {
            case CreationAccountResult.Success:
                AnsiConsole.MarkupLine("[green]Account created successfully![/]");
                break;
            case CreationAccountResult.UserDoesNotExist:
                AnsiConsole.MarkupLine("[red]User with the name does not exist![/]");
                break;
        }
    }
}