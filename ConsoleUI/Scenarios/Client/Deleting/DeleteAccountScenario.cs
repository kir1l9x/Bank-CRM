using Services.Controllers;
using Services.Controllers.OperationResults;
using Spectre.Console;

namespace ConsoleUI.Scenarios.Client.Deleting;

public class DeleteAccountScenario : IScenario
{
    private readonly IClientController _clientController;

    public DeleteAccountScenario(IClientController clientController)
    {
        _clientController = clientController;
    }

    public string Name => "Delete account";

    public void Run()
    {
        string accountNumberToDelete = AnsiConsole.Ask<string>("Enter account number: ");
        string checkForConfidence = AnsiConsole.Ask<string>("Do you sure you want to delete account? [y/n]");
        if (checkForConfidence.Equals("n", StringComparison.OrdinalIgnoreCase))
        {
            return;
        }

        if (checkForConfidence.Equals("y", StringComparison.OrdinalIgnoreCase))
        {
            DeleteAccountResult result = _clientController.DeleteAccount(accountNumberToDelete);

            switch (result)
            {
                case DeleteAccountResult.Success:
                    AnsiConsole.MarkupLine("[green]Success[/]");
                    break;
                case DeleteAccountResult.AccountDoesntExist:
                    AnsiConsole.MarkupLine("[yellow]Account number doesn't exist[/]");
                    break;
                case DeleteAccountResult.UserIsNotOwner:
                    AnsiConsole.MarkupLine("[red] You are not the owner of this account[/]");
                    break;
                case DeleteAccountResult.BalanceGreaterThanZero:
                    AnsiConsole.MarkupLine("[yellow] Before deleting you need to transfer your money to another account[/]");
                    break;
                case DeleteAccountResult.BalanceLowerThanZero:
                    AnsiConsole.MarkupLine("[yellow] Before deleting you need to close the debt[/]");
                    break;
            }
        }
    }
}