using Services.Controllers;
using Services.Controllers.OperationResults;
using Spectre.Console;

namespace ConsoleUI.Scenarios.Client.Show;

public class ShowBalanceScenario : IScenario
{
    private readonly IClientController _clientController;

    public ShowBalanceScenario(IClientController clientController)
    {
        _clientController = clientController;
    }

    public string Name => "Check Account Balance";

    public void Run()
    {
        string accountNumber = AnsiConsole.Ask<string>("Enter the account number:");
        CheckBalanceResult result = _clientController.ShowBalance(accountNumber);
        switch (result)
        {
            case CheckBalanceResult.Success successResult:
                AnsiConsole.MarkupLine($"[green]Success![/] The balance of account [yellow]{accountNumber}[/] is [blue]{successResult.Balance}[/].");
                break;

            case CheckBalanceResult.AccountDoesNotExist:
                AnsiConsole.MarkupLine($"[red]Error:[/] Account [yellow]{accountNumber}[/] does not exist.");
                break;

            case CheckBalanceResult.UserNotOwner:
                AnsiConsole.MarkupLine($"[red]Access Denied:[/] You are not the owner of account [yellow]{accountNumber}[/].");
                break;
        }
    }
}
