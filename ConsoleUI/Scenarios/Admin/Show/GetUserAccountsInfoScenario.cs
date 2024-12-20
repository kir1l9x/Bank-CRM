using Services.Controllers;
using Services.Controllers.OperationResults;
using Spectre.Console;

namespace ConsoleUI.Scenarios.Admin.Show;

public class GetUserAccountsInfoScenario : IScenario
{
    private readonly IAdminController _adminController;

    public GetUserAccountsInfoScenario(IAdminController adminController)
    {
        _adminController = adminController;
    }

    public string Name => "Get User Accounts Info";

    public void Run()
    {
        string username = AnsiConsole.Ask<string>("Enter user name: ");
        GetAccountsResult result = _adminController.GetUserAccounts(username);
        switch (result)
        {
            case GetAccountsResult.Success success:
                RenderAccountsTable(success.AccountsInfo);
                break;

            case GetAccountsResult.UserDoesNotExist:
                AnsiConsole.MarkupLine("[red]Error: The specified user does not exist![/]");
                break;

            case GetAccountsResult.UserDontHaveAccounts:
                AnsiConsole.MarkupLine("[yellow]The user has no accounts.[/]");
                break;
        }
    }

    private void RenderAccountsTable(string accountsData)
    {
        string[] accounts = accountsData.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        Table table = new Table()
            .Title("[bold]User Accounts Info[/]")
            .AddColumn("ID")
            .AddColumn("Account Number")
            .AddColumn("User ID")
            .AddColumn("Balance");
        foreach (string account in accounts)
        {
            string[] fields = account.Split('\t');
            table.AddRow(fields[0], fields[1], fields[2], fields[3]);
        }

        AnsiConsole.Write(table);
    }
}
