using Entities.Users;
using Services.Controllers;
using Services.Users;
using System.Diagnostics.CodeAnalysis;

namespace ConsoleUI.Scenarios.Admin.Show;

public class FindUserByIdScenarioProvider : IScenarioProvider
{
    private readonly IAdminController _adminController;
    private readonly ICurrentUserService _currentUserService;

    public FindUserByIdScenarioProvider(IAdminController adminController, ICurrentUserService currentUserService)
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

        scenario = new FindUserByIdScenario(_adminController);
        return true;
    }
}