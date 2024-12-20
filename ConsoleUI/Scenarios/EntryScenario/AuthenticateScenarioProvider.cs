using Services.Controllers;
using Services.Users;
using System.Diagnostics.CodeAnalysis;

namespace ConsoleUI.Scenarios.EntryScenario;

public class AuthenticateScenarioProvider : IScenarioProvider
{
    private readonly INonAuthorizedController _nonAuthorizedController;
    private readonly ICurrentUserService _currentUserService;

    public AuthenticateScenarioProvider(INonAuthorizedController nonAuthorizedController, ICurrentUserService currentUserService)
    {
        _nonAuthorizedController = nonAuthorizedController;
        _currentUserService = currentUserService;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentUserService.User is not null)
        {
            scenario = null;
            return false;
        }

        scenario = new AuthenticateScenario(_nonAuthorizedController, _currentUserService);
        return true;
    }
}