using Services.Controllers;
using Spectre.Console;

namespace ConsoleUI.Scenarios.Admin.Show;

public class ShowAuditLogsByDateScenario : IScenario
{
    private readonly IAdminController _adminController;

    public ShowAuditLogsByDateScenario(IAdminController adminController)
    {
        _adminController = adminController;
    }

    public string Name => "Show audit logs by date";

    public void Run()
    {
        DateTime startDate = AnsiConsole.Ask<DateTime>("Enter start date (yyyy-MM-dd):");
        DateTime endDate = AnsiConsole.Ask<DateTime>("Enter end date (yyyy-MM-dd):");
        string logsByDate = _adminController.ShowAuditLogsByDate(startDate, endDate);
        if (string.IsNullOrWhiteSpace(logsByDate))
        {
            AnsiConsole.MarkupLine("[red]No audit logs found in the specified date range.[/]");
            return;
        }

        string[] logs = logsByDate.Split("\t", StringSplitOptions.RemoveEmptyEntries);
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