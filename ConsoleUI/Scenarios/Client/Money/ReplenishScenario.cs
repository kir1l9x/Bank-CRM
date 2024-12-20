using Services.Controllers;
using Services.Controllers.OperationResults;
using Spectre.Console;

namespace ConsoleUI.Scenarios.Client.Money;

public class ReplenishScenario : IScenario
{
    private readonly IClientController _clientController;

    public ReplenishScenario(IClientController clientController)
    {
        _clientController = clientController;
    }

    public string Name => "Replenish";

    public void Run()
    {
        string accountNumber = AnsiConsole.Ask<string>("Enter account number: ");
        string replenishAmount = AnsiConsole.Ask<string>("Enter the replenishment amount: ");
        ReplenishResult result = _clientController.Replenish(accountNumber, replenishAmount);
        switch (result)
        {
            case ReplenishResult.Success:
                AnsiConsole.MarkupLine("[green]Success replenished![/]");
                break;
            case ReplenishResult.Failure:
                AnsiConsole.MarkupLine("[red]Failure replenished! Incorrect account number![/]");
                break;
        }
    }
}