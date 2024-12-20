using Services.Controllers;
using Spectre.Console;

namespace ConsoleUI.Scenarios.Client.Show;

public class ShowTransactionsScenario : IScenario
{
    private readonly IClientController _clientController;

    public ShowTransactionsScenario(IClientController clientController)
    {
        _clientController = clientController;
    }

    public string Name => "Show My Transactions";

    public void Run()
    {
        string result = _clientController.ShowTransactions();
        if (string.IsNullOrWhiteSpace(result))
        {
            AnsiConsole.MarkupLine("[red]You have no transactions to display.[/]");
            return;
        }

        string[] transactions = result.Split("\t", StringSplitOptions.RemoveEmptyEntries);
        var table = new Table();
        table.Border(TableBorder.Rounded);
        table.AddColumn("Transaction ID");
        table.AddColumn("Account ID");
        table.AddColumn("Amount");
        table.AddColumn("Type");
        table.AddColumn("Date");
        foreach (string transaction in transactions)
        {
            string[] details = transaction.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            if (details.Length == 5)
            {
                table.AddRow(
                    details[0],
                    details[1],
                    details[2],
                    details[3],
                    details[4]);
            }
        }

        AnsiConsole.Write(table);
    }
}
