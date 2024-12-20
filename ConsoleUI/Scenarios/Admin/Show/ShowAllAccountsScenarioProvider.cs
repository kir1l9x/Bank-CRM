using Entities.Users;
using Services.Controllers;
using Services.Users;
using System.Diagnostics.CodeAnalysis;

namespace ConsoleUI.Scenarios.Admin.Show;

public class ShowAllAccountsScenarioProvider : IScenarioProvider
{
    private readonly IAdminController _adminController;
    private readonly ICurrentUserService _currentUserService;

    public ShowAllAccountsScenarioProvider(IAdminController adminController, ICurrentUserService currentUserService)
    {
        _adminController = adminController;
        _currentUserService = currentUserService;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentUserService.User?.Role is not UserRole.Admin)
        {
            scenario = null;
            return false;
        }

        scenario = new ShowAllAccountsScenario(_adminController);
        return true;
    }
}