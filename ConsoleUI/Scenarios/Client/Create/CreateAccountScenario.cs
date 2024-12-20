using Services.Controllers;

namespace ConsoleUI.Scenarios.Client.Create;

public class CreateAccountScenario : IScenario
{
    private readonly IClientController _clientController;

    public CreateAccountScenario(IClientController clientController)
    {
        _clientController = clientController;
    }

    public string Name => "Create account";

    public void Run()
    {
        _clientController.CreateAccount();
    }
}