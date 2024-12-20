using Services.Controllers;
using Spectre.Console;

namespace ConsoleUI.Scenarios.Client.Show;

public class ShowAccountsScenario : IScenario
{
    private readonly IClientController _clientController;

    public ShowAccountsScenario(IClientController clientController)
    {
        _clientController = clientController;
    }

    public string Name => "Show My Accounts";

    public void Run()
    {
        string result = _clientController.ShowAccounts();
        if (string.IsNullOrWhiteSpace(result))
        {
            AnsiConsole.MarkupLine("[red]You have no accounts.[/]");
            return;
        }

        string[] accounts = result.Split("\t", StringSplitOptions.RemoveEmptyEntries);
        var table = new Table();
        table.Border(TableBorder.Rounded);
        table.AddColumn("Account ID");
        table.AddColumn("Account Number");
        table.AddColumn("Balance");
        foreach (string account in accounts)
        {
            string[] details = account.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            if (details.Length == 4)
            {
                table.AddRow(
                    details[0],
                    details[1],
                    details[3]);
            }
        }

        AnsiConsole.Write(table);
    }
}