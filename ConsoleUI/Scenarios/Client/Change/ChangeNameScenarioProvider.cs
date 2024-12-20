using Entities.Users;
using Services.Controllers;
using Services.Users;
using System.Diagnostics.CodeAnalysis;

namespace ConsoleUI.Scenarios.Client.Change;

public class ChangeNameScenarioProvider : IScenarioProvider
{
    private readonly IClientController _clientController;
    private readonly ICurrentUserService _currentUserService;

    public ChangeNameScenarioProvider(IClientController clientController, ICurrentUserService currentUserService)
    {
        _clientController = clientController;
        _currentUserService = currentUserService;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentUserService.User?.Role is not UserRole.Client)
        {
            scenario = null;
            return false;
        }

        scenario = new ChangeNameScenario(_clientController);
        return true;
    }
}