using Services.Controllers.OperationResults;

namespace Services.Controllers;

public interface IClientController
{
    void ChangePassword(string newPassword);

    void ChangeName(string newName);

    void CreateAccount();

    DeleteAccountResult DeleteAccount(string accountNumber);

    CheckBalanceResult ShowBalance(string accountNumber);

    string ShowTransactions();

    string ShowAccounts();

    string ShowClientMenu();

    TransferResult Transfer(string fromAccountNumber, string toAccountNumber, string amount);

    ReplenishResult Replenish(string accountNumber, string amount);

    void LogOut();
}