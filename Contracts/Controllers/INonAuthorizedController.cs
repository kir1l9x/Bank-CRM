using Services.Controllers.OperationResults;

namespace Services.Controllers;

public interface INonAuthorizedController
{
    AuthenticationResult Authenticate(string username, string password);

    AuthenticationResult LogIn(string username, string password);

    void LogOut();
}