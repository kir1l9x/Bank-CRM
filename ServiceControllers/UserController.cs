using Entities;
using Entities.Accounts;
using Entities.AuditLogs;
using Entities.Transactions;
using Entities.Users;
using Services.Accounts;
using Services.AuditLogs;
using Services.Controllers;
using Services.Controllers.OperationResults;
using Services.Transactions;
using Services.Users;
using Spectre.Console;

namespace ServiceControllers;

public class UserController : INonAuthorizedController, IClientController, IAdminController
{
    private readonly IUserService _userService;
    private readonly IAccountService _accountService;
    private readonly ITransactionService _transactionService;
    private readonly IAuditLogService _auditLogService;
    private readonly CurrentUserService _currentUserService;

    public UserController(
        IUserService userService,
        IAccountService accountService,
        ITransactionService transactionService,
        IAuditLogService auditLogService,
        CurrentUserService currentUserService)
    {
        _userService = userService;
        _accountService = accountService;
        _transactionService = transactionService;
        _auditLogService = auditLogService;
        _currentUserService = currentUserService;
    }

    public AuthenticationResult Authenticate(string username, string password)
    {
        User? user = _userService.GetUserByName(username);
        if (user is null)
        {
            _auditLogService.Create(null, $"Attempt to authenticate with username {username}, but it doesn't exist");
            return new AuthenticationResult.UserWithNameDoesNotExist();
        }

        User? existUser = _userService.GetUser(username, password);
        if (existUser is null)
        {
            _auditLogService.Create(null, $"Attempt to authenticate with username {username}, but wrong password");
            return new AuthenticationResult.WrongPassword();
        }

        _auditLogService.Create(user.Id, $"Authenticated with username {username}");
        return new AuthenticationResult.Success(user);
    }

    public AuthenticationResult LogIn(string username, string password)
    {
        User? user = _userService.Create(username, password);
        if (user is null)
        {
            _auditLogService.Create(Guid.Empty, "Attempt to log in with username {username}, but it already exists");
            return new AuthenticationResult.UserWithNameExists();
        }

        _auditLogService.Create(user.Id, $"Created and logged into user {username}");
        return new AuthenticationResult.Success(user);
    }

    public void ChangePassword(string newPassword)
    {
        User user = EnsureUserIsAuthenticated();
        _userService.ChangePassword(user, newPassword);
        _auditLogService.Create(user.Id, $"Password changed to {newPassword}");
    }

    public void ChangeName(string newName)
    {
        User user = EnsureUserIsAuthenticated();
        _userService.ChangeUserName(user, newName);
        _auditLogService.Create(user.Id, $"Username changed to {newName}");
    }

    public void CreateAccount()
    {
        User user = EnsureUserIsAuthenticated();
        Account account = _accountService.Create(user);
        _auditLogService.Create(user.Id, $"Account {account.AccountNumber} created");
    }

    public CreationUserResult CreateNewUser(string username, string password)
    {
        User host = EnsureUserIsAuthenticated();
        User? userToCreate = _userService.Create(username, password);
        if (userToCreate is null)
        {
            _auditLogService.Create(Guid.Empty, "Admin tried to create new user with username {username}, but user with such name exists");
            return new CreationUserResult.UserWithThisNameExist();
        }

        _auditLogService.Create(host.Id, $"Admin created new user {username}");
        return new CreationUserResult.Success(userToCreate);
    }

    public CreationAccountResult CreateNewAccount(string username)
    {
        User host = EnsureUserIsAuthenticated();
        User? userToCreateAccount = _userService.GetUserByName(username);
        if (userToCreateAccount is null)
        {
            _auditLogService.Create(host.Id, $"Admin tried to create account for user {username}, but it doesn't exist");
            return new CreationAccountResult.UserDoesNotExist();
        }

        Account account = _accountService.Create(userToCreateAccount);
        _auditLogService.Create(host.Id, $"Account {account.AccountNumber} created by admin");
        return new CreationAccountResult.Success(account);
    }

    public FindingUserResult FindUser(string username)
    {
        User host = EnsureUserIsAuthenticated();
        User? userToFind = _userService.GetUserByName(username);
        if (userToFind is null)
        {
            _auditLogService.Create(host.Id, $"Admin tried to find user {username}, but it doesn't exist");
            return new FindingUserResult.Failed();
        }
        else
        {
            _auditLogService.Create(host.Id, $"Admin found user {username}");
        }

        return new FindingUserResult.Success(userToFind);
    }

    public FindingUserResult FindUser(Guid userId)
    {
        User host = EnsureUserIsAuthenticated();
        User? userToFind = _userService.GetUserById(userId);
        if (userToFind is null)
        {
            _auditLogService.Create(host.Id, $"Admin tried to find user {userId}, but it doesn't exist");
            return new FindingUserResult.Failed();
        }
        else
        {
            _auditLogService.Create(host.Id, $"Admin found user {userId}");
        }

        return new FindingUserResult.Success(userToFind);
    }

    public GetAccountsResult GetUserAccounts(string username)
    {
        User host = EnsureUserIsAuthenticated();
        User? user = _userService.GetUserByName(username);
        if (user is null)
        {
            _auditLogService.Create(host.Id, $"Admin tried to get user accounts, but user {username} does not exist");
            return new GetAccountsResult.UserDoesNotExist();
        }

        IEnumerable<Account> accounts = _accountService.GetUsersAccounts(user.Id);
        IEnumerable<Account> enumerable = accounts.ToList();
        if (!enumerable.Any())
        {
            _auditLogService.Create(host.Id, $"Admin checked accounts for user {username}, but the user has no accounts.");
            return new GetAccountsResult.UserDontHaveAccounts();
        }

        _auditLogService.Create(host.Id, $"Admin checked accounts for user {username}.");

        return new GetAccountsResult.Success(string.Join("\t", enumerable.Select(account => account.ToString())));
    }

    public ChangingUserResult ChangeUserPassword(string username, string newPassword)
    {
        User host = EnsureUserIsAuthenticated();
        User? user = _userService.GetUserByName(username);
        if (user is null)
        {
            _auditLogService.Create(host.Id, $"Admin tried to change user password, but user {username} does not exist");
            return new ChangingUserResult.Failure();
        }

        _userService.ChangePassword(user, newPassword);
        _auditLogService.Create(host.Id, $"Admin changed user {username} password");

        return new ChangingUserResult.Success();
    }

    public ChangingUserResult ChangeUserName(string username, string newUsername)
    {
        User host = EnsureUserIsAuthenticated();
        User? user = _userService.GetUserByName(username);
        if (user is null)
        {
            _auditLogService.Create(host.Id, $"Admin tried to change user name, but user {username} does not exist");
            return new ChangingUserResult.Failure();
        }

        _userService.ChangeUserName(user, newUsername);
        _auditLogService.Create(host.Id, $"Admin changed user name to {newUsername}");

        return new ChangingUserResult.Success();
    }

    public ChangingUserResult ChangeUserRole(string username, UserRole newRole)
    {
        User host = EnsureUserIsAuthenticated();
        User? user = _userService.GetUserByName(username);
        if (user is null)
        {
            _auditLogService.Create(host.Id, $"Admin tried to change user role, but user {username} does not exist");
            return new ChangingUserResult.Failure();
        }

        _userService.ChangeUserRole(user, newRole);
        _auditLogService.Create(host.Id, $"Admin changed user {username} role");

        return new ChangingUserResult.Success();
    }

    public void ChangeOwnAdminName(string newUsername)
    {
        User host = EnsureUserIsAuthenticated();
        _userService.ChangeUserName(host, newUsername);
        _auditLogService.Create(host.Id, $"Admin changed own name to {newUsername}");
    }

    public void ChangeOwnAdminPassword(string newPassword)
    {
        User host = EnsureUserIsAuthenticated();
        _userService.ChangePassword(host, newPassword);
        _auditLogService.Create(host.Id, $"Admin changed own password");
    }

    public DeleteUserResult DeleteUser(string username)
    {
        User host = EnsureUserIsAuthenticated();
        User? userToDelete = _userService.GetUserByName(username);
        if (userToDelete is null)
        {
            _auditLogService.Create(host.Id, $"Admin tried to delete user {username}, but it doesn't exist");
            return new DeleteUserResult.Failure();
        }

        IEnumerable<Account> userAccounts = _accountService.GetUsersAccounts(userToDelete.Id);
        _userService.DeleteUser(userToDelete);
        foreach (Account account in userAccounts)
        {
            _accountService.Delete(account);
        }

        _auditLogService.Create(host.Id, $"Admin deleted user {username}");
        return new DeleteUserResult.Success();
    }

    DeleteAccountResult IAdminController.DeleteAccount(string accountNumber)
    {
        User host = EnsureUserIsAuthenticated();
        Account? accountToDelete = _accountService.GetUserAccount(accountNumber);
        if (accountToDelete is null)
        {
            _auditLogService.Create(host.Id, $"Admin tried to delete account {accountNumber}, but it doesn't exist");
            return new DeleteAccountResult.AccountDoesntExist();
        }

        _accountService.Delete(accountToDelete);
        _auditLogService.Create(host.Id, $"Admin deleted account {accountNumber}");

        return new DeleteAccountResult.Success();
    }

    public string ShowAdminMenu()
    {
        string menuChoice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[bold green]Admin Menu[/]")
                .PageSize(10)
                .AddChoices(
                    "Create New User",
                    "Create New Account",
                    "Find User",
                    "Get User Accounts",
                    "Change User Password",
                    "Change User Name",
                    "Change User Role",
                    "Show All Users",
                    "Show All Accounts",
                    "Show All Audit Logs",
                    "Show User Audit Logs",
                    "Show Audit Logs By Date",
                    "Show Account Transactions",
                    "Show Account Transactions By Date",
                    "Delete User",
                    "Delete Account",
                    "Log Out"));

        return menuChoice;
    }

    public string ShowAllAuditLogs()
    {
        User host = EnsureUserIsAuthenticated();
        IEnumerable<AuditLog> allLogs = _auditLogService.GetAll();
        IEnumerable<string> formattedLogs = allLogs.Select(log => log.ToString());
        _auditLogService.Create(host.Id, $"Admin checked all logs");
        return string.Join("\t", formattedLogs);
    }

    public string ShowUserAuditLogs(string username)
    {
        User host = EnsureUserIsAuthenticated();
        User? userToFind = _userService.GetUserByName(username);
        if (userToFind is null)
        {
            _auditLogService.Create(host.Id, $"Admin tried to show user audit logs, but user does not exist");
            return "User does not exist";
        }

        IEnumerable<AuditLog> allLogs = _auditLogService.GetAuditLogsByUserId(userToFind.Id);
        IEnumerable<string> formattedLogs = allLogs.Select(log => log.ToString());
        _auditLogService.Create(host.Id, $"Admin checked all user logs");
        return string.Join("\t", formattedLogs);
    }

    public string ShowAuditLogsByDate(DateTime startDate, DateTime endDate)
    {
        User host = EnsureUserIsAuthenticated();
        IEnumerable<AuditLog> allLogs = _auditLogService.GetAuditLogsByDateRange(startDate, endDate);
        IEnumerable<string> formattedLogs = allLogs.Select(log => log.ToString());
        _auditLogService.Create(host.Id, $"Admin checked audit logs by date");
        return string.Join("\t", formattedLogs);
    }

    public GetAccountTransactionsResult ShowAccountTransactions(string accountNumber)
    {
        User host = EnsureUserIsAuthenticated();
        Account? accountToFind = _accountService.GetUserAccount(accountNumber);
        if (accountToFind is null)
        {
            _auditLogService.Create(host.Id, $"Admin tried to find account {accountNumber}, but it doesn't exist");
            return new GetAccountTransactionsResult.AccountDoesNotExist();
        }

        IEnumerable<Transaction> transactions = _transactionService.GetTransactionsByAccountId(accountToFind.Id);
        IEnumerable<Transaction> enumerable = transactions.ToList();
        if (!enumerable.Any())
        {
            return new GetAccountTransactionsResult.TransactionListIsEmpty();
        }

        IEnumerable<string> formattedTransactions = enumerable.Select(tx => tx.ToString());
        _auditLogService.Create(host.Id, $"Admin checked transactions for account {accountNumber}");

        return new GetAccountTransactionsResult.Success(string.Join("\t", formattedTransactions));
    }

    public GetAccountTransactionsResult ShowAccountTransactionsByDate(string accountNumber, DateTime startDate, DateTime endDate)
    {
        User host = EnsureUserIsAuthenticated();
        Account? accountToFind = _accountService.GetUserAccount(accountNumber);
        if (accountToFind is null)
        {
            _auditLogService.Create(host.Id, $"Admin tried to find account {accountNumber}, but it doesn't exist");
            return new GetAccountTransactionsResult.AccountDoesNotExist();
        }

        IEnumerable<Transaction> transactions = _transactionService.GetAccountTransactionsByDateRange(accountToFind.Id, startDate, endDate);
        IEnumerable<Transaction> enumerable = transactions.ToList();
        if (!enumerable.Any())
        {
            return new GetAccountTransactionsResult.TransactionListIsEmpty();
        }

        IEnumerable<string> formattedTransactions = enumerable.Select(tx => tx.ToString());
        _auditLogService.Create(host.Id, $"Admin checked transactions for account {accountNumber} from {startDate:yyyy-MM-dd} to {endDate:yyyy-MM-dd}");

        return new GetAccountTransactionsResult.Success(string.Join("\t", formattedTransactions));
    }

    public string ShowAllUsers()
    {
        User host = EnsureUserIsAuthenticated();
        IEnumerable<User> allUsers = _userService.GetAll();
        IEnumerable<string> formattedUsers = allUsers.Select(user => user.ToString());
        _auditLogService.Create(host.Id, $"Admin checked all users");

        return string.Join("\t", formattedUsers);
    }

    public string ShowAllAccounts()
    {
        User host = EnsureUserIsAuthenticated();
        IEnumerable<Account> allAccounts = _accountService.GetAll();
        IEnumerable<string> formattedAccounts = allAccounts.Select(account => account.ToString());
        _auditLogService.Create(host.Id, $"Admin checked all accounts");

        return string.Join("\t", formattedAccounts);
    }

    void IAdminController.LogOut()
    {
        _currentUserService.User = null;
    }

    DeleteAccountResult IClientController.DeleteAccount(string accountNumber)
    {
        User currentUser = EnsureUserIsAuthenticated();
        Account? accountToDelete = _accountService.GetUserAccount(accountNumber);
        if (accountToDelete is null)
        {
            _auditLogService.Create(currentUser.Id, $"User tried to delete account {accountNumber}, but it does not exist");
            return new DeleteAccountResult.AccountDoesntExist();
        }

        if (accountToDelete.UserId != currentUser.Id)
        {
            _auditLogService.Create(currentUser.Id, $"User tried to delete account {accountNumber}, but the current user is not owner");
            return new DeleteAccountResult.UserIsNotOwner();
        }

        if (accountToDelete.Balance > new Money(0))
        {
            _auditLogService.Create(currentUser.Id, "User tried to delete account, but balance is greater than zero");
            return new DeleteAccountResult.BalanceGreaterThanZero();
        }

        if (accountToDelete.Balance < new Money(0))
        {
            _auditLogService.Create(currentUser.Id, "User tried to delete account, but balance is less than zero");
            return new DeleteAccountResult.BalanceLowerThanZero();
        }

        _accountService.Delete(accountToDelete);
        _auditLogService.Create(currentUser.Id, $"User deleted account {accountNumber}");

        return new DeleteAccountResult.Success();
    }

    public CheckBalanceResult ShowBalance(string accountNumber)
    {
        User currentUser = EnsureUserIsAuthenticated();
        Account? checkAccount = _accountService.GetUserAccount(accountNumber);
        if (checkAccount is null)
        {
            _auditLogService.Create(currentUser.Id, $"User tried to check account {accountNumber} balance, but it does not exist");
            return new CheckBalanceResult.AccountDoesNotExist();
        }

        if (checkAccount.UserId != currentUser.Id)
        {
            _auditLogService.Create(currentUser.Id, $"User tried to check account {accountNumber} balance, but the current user is not owner");
            return new CheckBalanceResult.UserNotOwner();
        }

        _auditLogService.Create(currentUser.Id, $"User checked account {accountNumber} balance");
        return new CheckBalanceResult.Success(checkAccount.Balance.ToString());
    }

    public string ShowTransactions()
    {
        User currentUser = EnsureUserIsAuthenticated();
        IEnumerable<Account> userAccounts = _accountService.GetUsersAccounts(currentUser.Id);
        var enumerable = userAccounts.ToList();
        if (enumerable.Count == 0)
        {
            _auditLogService.Create(currentUser.Id, $"User tried to check transactions, but no accounts exist");
            return string.Empty;
        }

        var allTransactions = new List<Transaction>();
        foreach (Account account in enumerable)
        {
            IEnumerable<Transaction> accountTransactions = _transactionService.GetTransactionsByAccountId(account.Id);
            allTransactions.AddRange(accountTransactions);
        }

        IEnumerable<string> formattedTransactions = allTransactions.Select(tx => tx.ToString());

        _auditLogService.Create(currentUser.Id, "User checked all transactions for their accounts");

        IEnumerable<string> transactions = formattedTransactions.ToList();
        return transactions.Any()
            ? string.Join("\t", transactions)
            : string.Empty;
    }

    public string ShowAccounts()
    {
        User currentUser = EnsureUserIsAuthenticated();
        IEnumerable<Account> userAccounts = _accountService.GetUsersAccounts(currentUser.Id);

        IEnumerable<Account> enumerable = userAccounts.ToList();
        if (!enumerable.Any())
        {
            _auditLogService.Create(currentUser.Id, "User tried to view accounts but has no accounts");
            return string.Empty;
        }

        IEnumerable<string> formattedAccounts = enumerable.Select(account => account.ToString());

        _auditLogService.Create(currentUser.Id, "User viewed his accounts");
        return string.Join("\t", formattedAccounts);
    }

    public string ShowClientMenu()
    {
        string menuChoice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[bold blue]Client Menu[/]")
                .PageSize(10)
                .AddChoices(
                    "Change Password",
                    "Change Name",
                    "Create Account",
                    "Delete Account",
                    "Show Balance",
                    "Show Transactions",
                    "Show Accounts",
                    "Transfer",
                    "Replenish",
                    "Log Out"));

        return menuChoice;
    }

    public TransferResult Transfer(string fromAccountNumber, string toAccountNumber, string amount)
    {
        User currentUser = EnsureUserIsAuthenticated();
        Account? accountFrom = _accountService.GetUserAccount(fromAccountNumber);
        if (accountFrom is null)
        {
            _auditLogService.Create(currentUser.Id, $"User tried to transfer from account {fromAccountNumber}, but it does not exist");
            return new TransferResult.IncorrectFromNumber();
        }

        if (accountFrom.UserId != currentUser.Id)
        {
            _auditLogService.Create(currentUser.Id, $"User tried to transfer from account {fromAccountNumber}, but user is not owner");
            return new TransferResult.UserIsNotOwner();
        }

        var moneyToTransfer = new Money(decimal.Parse(amount));
        if (accountFrom.Balance < moneyToTransfer)
        {
            _auditLogService.Create(currentUser.Id, $"User tried to transfer from account {fromAccountNumber}, but balance was lower than transfer amount");
            return new TransferResult.BalanceIsLowerThanTransferAmount();
        }

        Account? accountTo = _accountService.GetUserAccount(toAccountNumber);
        if (accountTo is null)
        {
            _auditLogService.Create(currentUser.Id, $"User tried to transfer to account {toAccountNumber}, but it does not exist");
            return new TransferResult.IncorrectToNumber();
        }

        Transaction transferTransactionFrom = _transactionService.Create(accountFrom.Id, moneyToTransfer, new TransactionType.WriteOff());
        Transaction transferTransactionTo = _transactionService.Create(accountTo.Id, moneyToTransfer, new TransactionType.Replenishment());
        _accountService.UpdateAccount(accountFrom.Id, transferTransactionFrom);
        _accountService.UpdateAccount(accountTo.Id, transferTransactionTo);
        return new TransferResult.Success();
    }

    public ReplenishResult Replenish(string accountNumber, string amount)
    {
        User currentUser = EnsureUserIsAuthenticated();
        Account? accountToReplenish = _accountService.GetUserAccount(accountNumber);
        if (accountToReplenish is null)
        {
            _auditLogService.Create(currentUser.Id, $"User tried to replenish to account {accountNumber}, but it does not exist");
            return new ReplenishResult.Failure();
        }

        var replenishMoney = new Money(decimal.Parse(amount));
        Transaction replenishTransaction = _transactionService.Create(accountToReplenish.Id, replenishMoney, new TransactionType.Replenishment());
        _accountService.UpdateAccount(accountToReplenish.Id, replenishTransaction);
        return new ReplenishResult.Success();
    }

    void IClientController.LogOut()
    {
        _currentUserService.User = null;
    }

    void INonAuthorizedController.LogOut()
    {
        Environment.Exit(0);
    }

    private User EnsureUserIsAuthenticated()
    {
        if (_currentUserService.User is null)
        {
            throw new UnauthorizedAccessException();
        }

        return _currentUserService.User;
    }
}