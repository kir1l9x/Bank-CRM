using Services.Controllers;
using Spectre.Console;

namespace ConsoleUI.Scenarios.Admin.Show;

public class ShowUserAuditLogsScenario : IScenario
{
    private readonly IAdminController _adminController;

    public ShowUserAuditLogsScenario(IAdminController adminController)
    {
        _adminController = adminController;
    }

    public string Name => "Show user audit logs";

    public void Run()
    {
        string username = AnsiConsole.Ask<string>("Enter username:");
        string userLogs = _adminController.ShowUserAuditLogs(username);

        if (userLogs == "User does not exist")
        {
            AnsiConsole.MarkupLine("[red]User does not exist.[/]");
            return;
        }

        if (string.IsNullOrWhiteSpace(userLogs))
        {
            AnsiConsole.MarkupLine("[red]No audit logs found for this user.[/]");
            return;
        }

        string[] logs = userLogs.Split("\t", StringSplitOptions.RemoveEmptyEntries);
        var table = new Table();
        table.Border(TableBorder.Rounded);
        table.AddColumn("ID");
        table.AddColumn("User ID");
        table.AddColumn("Action Description");
        table.AddColumn("Action Time");

        foreach (string log in logs)
        {
            string[] logDetails = log.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            if (logDetails.Length == 4)
            {
                table.AddRow(
                    logDetails[0],
                    logDetails[1],
                    logDetails[2],
                    logDetails[3]);
            }
        }

        AnsiConsole.Write(table);
    }
}
