using Services.Controllers;
using Services.Controllers.OperationResults;
using Spectre.Console;

namespace ConsoleUI.Scenarios.Admin.Show;

public class ShowAccountTransactionsScenario : IScenario
{
    private readonly IAdminController _adminController;

    public ShowAccountTransactionsScenario(IAdminController adminController)
    {
        _adminController = adminController;
    }

    public string Name => "Show All Account Transactions";

    public void Run()
    {
        string accountNumber = AnsiConsole.Ask<string>("Enter account number: ");
        GetAccountTransactionsResult result = _adminController.ShowAccountTransactionsByDate(accountNumber, DateTime.MinValue, DateTime.MaxValue);
        switch (result)
        {
            case GetAccountTransactionsResult.AccountDoesNotExist:
                AnsiConsole.MarkupLine("[red]Error: Account does not exist![/]");
                break;

            case GetAccountTransactionsResult.TransactionListIsEmpty:
                AnsiConsole.MarkupLine("[yellow]No transactions found for this account.[/]");
                break;

            case GetAccountTransactionsResult.Success success:
                RenderTransactionsTable(success.TransactionsInfo);
                break;
        }
    }

    private void RenderTransactionsTable(string transactionsData)
    {
        string[] transactions = transactionsData.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        Table table = new Table()
            .Title("[bold]All Account Transactions[/]")
            .AddColumn("Transaction ID")
            .AddColumn("Account ID")
            .AddColumn("Amount")
            .AddColumn("Type")
            .AddColumn("Date");
        foreach (string transaction in transactions)
        {
            string[] fields = transaction.Split('\t');
            table.AddRow(fields[0], fields[1], fields[2], fields[3], fields[4]);
        }

        AnsiConsole.Write(table);
    }
}
