using Services.Controllers;
using Services.Controllers.OperationResults;
using Spectre.Console;

namespace ConsoleUI.Scenarios.Client.Money;

public class TransferScenario : IScenario
{
    private readonly IClientController _clientController;

    public TransferScenario(IClientController clientController)
    {
        _clientController = clientController;
    }

    public string Name => "Transfer";

    public void Run()
    {
        string fromAccountNumber = AnsiConsole.Ask<string>("Enter from account number: ");
        string toAccountNumber = AnsiConsole.Ask<string>("Enter to account number: ");
        string transferAmount = AnsiConsole.Ask<string>("Enter the transfer amount: ");
        TransferResult result = _clientController.Transfer(fromAccountNumber, toAccountNumber, transferAmount);
        switch (result)
        {
            case TransferResult.Success:
                AnsiConsole.MarkupLine("[green]Success transfer![/]");
                break;
            case TransferResult.IncorrectFromNumber:
                AnsiConsole.MarkupLine("[red]Failure transfer! Incorrect from account number![/]");
                break;
            case TransferResult.IncorrectToNumber:
                AnsiConsole.MarkupLine("[red]Failure transfer! Incorrect to account number![/]");
                break;
            case TransferResult.UserIsNotOwner:
                AnsiConsole.MarkupLine("[red]From account is not yours![/]");
                break;
            case TransferResult.BalanceIsLowerThanTransferAmount:
                AnsiConsole.MarkupLine("[red]Balance is lower than transfer amount[/]");
                break;
        }
    }
}