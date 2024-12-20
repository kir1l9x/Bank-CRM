using Services.Controllers;
using Spectre.Console;

namespace ConsoleUI.Scenarios.Admin.Show;

public class ShowAllAccountsScenario : IScenario
{
    private readonly IAdminController _adminController;

    public ShowAllAccountsScenario(IAdminController adminController)
    {
        _adminController = adminController;
    }

    public string Name => "Show all accounts";

    public void Run()
    {
        string allAccounts = _adminController.ShowAllAccounts();
        if (string.IsNullOrWhiteSpace(allAccounts))
        {
            AnsiConsole.MarkupLine("[red]No accounts found.[/]");
            return;
        }

        string[] accounts = allAccounts.Split("\t", StringSplitOptions.RemoveEmptyEntries);
        var table = new Table();
        table.Border(TableBorder.Rounded);
        table.AddColumn("ID");
        table.AddColumn("Account Number");
        table.AddColumn("User ID");
        table.AddColumn("Balance");
        foreach (string account in accounts)
        {
            string[] accountDetails = account.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            if (accountDetails.Length == 4)
            {
                table.AddRow(
                    accountDetails[0],
                    accountDetails[1],
                    accountDetails[2],
                    accountDetails[3]);
            }
        }

        AnsiConsole.Write(table);
    }
}