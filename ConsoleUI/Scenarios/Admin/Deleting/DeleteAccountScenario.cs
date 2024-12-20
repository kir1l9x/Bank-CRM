using Services.Controllers;
using Services.Controllers.OperationResults;
using Spectre.Console;

namespace ConsoleUI.Scenarios.Admin.Deleting;

public class DeleteAccountScenario : IScenario
{
    private readonly IAdminController _adminController;

    public DeleteAccountScenario(IAdminController adminController)
    {
        _adminController = adminController;
    }

    public string Name => "Delete account";

    public void Run()
    {
        string accountNumber = AnsiConsole.Ask<string>("Enter account number to delete: ");
        DeleteAccountResult result = _adminController.DeleteAccount(accountNumber);
        switch (result)
        {
            case DeleteAccountResult.Success:
                AnsiConsole.MarkupLine("[green]Success[/]");
                break;
            case DeleteAccountResult.AccountDoesntExist:
                AnsiConsole.MarkupLine("[red]Account number doesn't exist[/]");
                break;
        }
    }
}