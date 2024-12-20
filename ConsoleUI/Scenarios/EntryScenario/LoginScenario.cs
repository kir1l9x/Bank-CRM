using Services.Controllers;
using Services.Controllers.OperationResults;
using Services.Users;
using Spectre.Console;

namespace ConsoleUI.Scenarios.EntryScenario;

public class LoginScenario : IScenario
{
    private readonly INonAuthorizedController _controller;
    private readonly ICurrentUserService _currentUserService;

    public LoginScenario(INonAuthorizedController controller, ICurrentUserService currentUserService)
    {
        _controller = controller;
        _currentUserService = currentUserService;
    }

    public string Name => "Login";

    public void Run()
    {
        string name = AnsiConsole.Ask<string>("Enter your username: ");
        string password = AnsiConsole.Ask<string>("Enter your password: ");
        AuthenticationResult authResult = _controller.LogIn(name, password);

        switch (authResult)
        {
            case AuthenticationResult.Success:
                AnsiConsole.MarkupLine($"[green]Welcome, {name}[/]");
                _currentUserService.User = authResult.AuthenticatedUser;
                break;
            case AuthenticationResult.UserWithNameExists:
                AnsiConsole.MarkupLine($"[red]Username with name {name} already exists[/]");
                break;
        }
    }
}