using Services.Controllers;
using Spectre.Console;

namespace ConsoleUI.Scenarios.Admin.Show;

public class ShowAllUsersScenario : IScenario
{
    private readonly IAdminController _adminController;

    public ShowAllUsersScenario(IAdminController adminController)
    {
        _adminController = adminController;
    }

    public string Name => "Show all users";

    public void Run()
    {
        string allUsers = _adminController.ShowAllUsers();

        if (string.IsNullOrWhiteSpace(allUsers))
        {
            AnsiConsole.MarkupLine("[red]No users found.[/]");
            return;
        }

        string[] users = allUsers.Split("\t", StringSplitOptions.RemoveEmptyEntries);
        var table = new Table();
        table.Border(TableBorder.Rounded);
        table.AddColumn("ID");
        table.AddColumn("Username");
        table.AddColumn("Role");
        table.AddColumn("Created On");

        foreach (string user in users)
        {
            string[] userDetails = user.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            if (userDetails.Length == 4)
            {
                table.AddRow(
                    userDetails[0],
                    userDetails[1],
                    userDetails[2],
                    userDetails[3]);
            }
        }

        AnsiConsole.Write(table);
    }
}