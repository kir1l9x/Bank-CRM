using Entities.Users;
using Services.Controllers;
using Services.Users;
using System.Diagnostics.CodeAnalysis;

namespace ConsoleUI.Scenarios.Client.Show;

public class ShowTransactionsScenarioProvider : IScenarioProvider
{
    private readonly IClientController _clientController;
    private readonly ICurrentUserService _currentUserService;

    public ShowTransactionsScenarioProvider(IClientController clientController, ICurrentUserService currentUserService)
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

        scenario = new ShowTransactionsScenario(_clientController);
        return true;
    }
}