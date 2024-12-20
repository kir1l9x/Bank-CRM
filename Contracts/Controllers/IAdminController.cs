using Entities.Users;
using Services.Controllers.OperationResults;

namespace Services.Controllers;

public interface IAdminController
{
    CreationUserResult CreateNewUser(string username, string password);

    CreationAccountResult CreateNewAccount(string username);

    FindingUserResult FindUser(string username);

    FindingUserResult FindUser(Guid userId);

    GetAccountsResult GetUserAccounts(string username);

    ChangingUserResult ChangeUserPassword(string username, string newPassword);

    ChangingUserResult ChangeUserName(string username, string newUsername);

    ChangingUserResult ChangeUserRole(string username, UserRole newRole);

    void ChangeOwnAdminName(string newUsername);

    void ChangeOwnAdminPassword(string newPassword);

    DeleteUserResult DeleteUser(string username);

    DeleteAccountResult DeleteAccount(string accountNumber);

    string ShowAdminMenu();

    string ShowAllAuditLogs();

    string ShowUserAuditLogs(string username);

    string ShowAuditLogsByDate(DateTime startDate, DateTime endDate);

    GetAccountTransactionsResult ShowAccountTransactions(string accountNumber);

    GetAccountTransactionsResult ShowAccountTransactionsByDate(string accountNumber, DateTime startDate, DateTime endDate);

    string ShowAllUsers();

    string ShowAllAccounts();

    void LogOut();
}