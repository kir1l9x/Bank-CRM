using Services.Controllers;
using Spectre.Console;

namespace ConsoleUI.Scenarios.Admin.Show;

public class ShowAllAuditLogsScenario : IScenario
{
    private readonly IAdminController _adminController;

    public ShowAllAuditLogsScenario(IAdminController adminController)
    {
        _adminController = adminController;
    }

    public string Name => "Show all audit logs";

    public void Run()
    {
        string allLogs = _adminController.ShowAllAuditLogs();

        if (string.IsNullOrWhiteSpace(allLogs))
        {
            AnsiConsole.MarkupLine("[red]No audit logs found.[/]");
            return;
        }

        string[] logs = allLogs.Split("\t", StringSplitOptions.RemoveEmptyEntries);
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