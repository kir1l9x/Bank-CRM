using Services.Controllers;
using Services.Controllers.OperationResults;
using Services.Users;
using Spectre.Console;

namespace ConsoleUI.Scenarios.EntryScenario;

public class AuthenticateScenario : IScenario
{
    private readonly INonAuthorizedController _controller;
    private readonly ICurrentUserService _currentUserService;

    public AuthenticateScenario(INonAuthorizedController controller, ICurrentUserService currentUserService)
    {
        _controller = controller;
        _currentUserService = currentUserService;
    }

    public string Name => "Authenticate";

    public void Run()
    {
        string name = AnsiConsole.Ask<string>("Enter your username: ");
        string password = AnsiConsole.Ask<string>("Enter your password: ");
        AuthenticationResult authResult = _controller.Authenticate(name, password);

        switch (authResult)
        {
            case AuthenticationResult.Success:
                AnsiConsole.MarkupLine($"[green]Welcome, {name}[/]");
                _currentUserService.User = authResult.AuthenticatedUser;
                break;
            case AuthenticationResult.UserWithNameDoesNotExist:
                AnsiConsole.MarkupLine($"[red]Username {name} does not exist[/]");
                break;
            case AuthenticationResult.WrongPassword:
                AnsiConsole.MarkupLine($"[red]Wrong password[/]");
                break;
        }
    }
}