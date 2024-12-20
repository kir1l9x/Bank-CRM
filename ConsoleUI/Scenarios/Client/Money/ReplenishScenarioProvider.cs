using Entities.Users;
using Services.Controllers;
using Services.Users;
using System.Diagnostics.CodeAnalysis;

namespace ConsoleUI.Scenarios.Client.Money;

public class ReplenishScenarioProvider : IScenarioProvider
{
    private readonly IClientController _clientController;
    private readonly ICurrentUserService _currentUserService;

    public ReplenishScenarioProvider(IClientController clientController, ICurrentUserService currentUserService)
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

        scenario = new ReplenishScenario(_clientController);
        return true;
    }
}