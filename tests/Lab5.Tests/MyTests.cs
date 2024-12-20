using Entities;
using Entities.Accounts;
using Entities.Users;
using Interfaces.Repositories;
using Moq;
using ServiceControllers;
using Services.Accounts;
using Services.AuditLogs;
using Services.Controllers.OperationResults;
using Services.Transactions;
using Services.Users;
using Xunit;

namespace Lab5.Tests;

public class MyTests
{
    private readonly Mock<IAccountsRepository> _accountsRepositoryMock;
    private readonly Mock<ITransactionsRepository> _transactionsRepositoryMock;
    private readonly Mock<IUsersRepository> _usersRepositoryMock;
    private readonly Mock<IAuditLogRepository> _auditLogRepositoryMock;
    private readonly UserController _clientController;
    private readonly AccountService _accountService;
    private readonly CurrentUserService _currentUserService;
    private readonly User _user;

    public MyTests()
    {
        _accountsRepositoryMock = new Mock<IAccountsRepository>();
        _transactionsRepositoryMock = new Mock<ITransactionsRepository>();
        _usersRepositoryMock = new Mock<IUsersRepository>();
        _auditLogRepositoryMock = new Mock<IAuditLogRepository>();
        User user = User.Builder()
            .SetUsername("bu")
            .SetHashPassword("52")
            .SetRole(100)
            .Build();
        _currentUserService = new CurrentUserService();
        _currentUserService.User = user;
        _user = user;
        _accountService = new AccountService(_accountsRepositoryMock.Object, _currentUserService);
        _clientController = new UserController(
            new UserService(_usersRepositoryMock.Object, _currentUserService),
            new AccountService(_accountsRepositoryMock.Object, _currentUserService),
            new TransactionService(_transactionsRepositoryMock.Object, _accountsRepositoryMock.Object, _currentUserService),
            new AuditLogService(_auditLogRepositoryMock.Object, _currentUserService),
            _currentUserService);
    }

    [Fact]

    public void ShouldReplenishAccountWithCorrectNumber_WhenClientReplenishAccount_ShouldReturnSuccess()
    {
        Account account = _accountService.Create(_user);
        _accountsRepositoryMock
            .Setup(repo => repo.GetById(account.Id))
            .Returns(account);
        _accountsRepositoryMock.Setup(repo => repo.GetByAccountNumber(account.AccountNumber)).Returns(account);
        ReplenishResult replenishResult = _clientController.Replenish(account.AccountNumber, "100");
        var expectedReplenishResult = new ReplenishResult.Success();

        var expectedResult = new Money(100m);
        Money actualResult = account.Balance;
        Assert.Equal(expectedResult, actualResult);
        Assert.Equal(expectedReplenishResult, replenishResult);
    }

    [Fact]

    public void ShouldTransferFromAccountToAccount_WhenClientTransferMoneyWithCorrectAmounts_ReturnsSuccessTransfer()
    {
        Account account = _accountService.Create(_user);
        Account otherAccount = _accountService.Create(_user);
        _accountsRepositoryMock
            .Setup(repo => repo.GetById(account.Id))
            .Returns(account);
        _accountsRepositoryMock
            .Setup(repo => repo.GetById(otherAccount.Id))
            .Returns(otherAccount);
        _accountsRepositoryMock
            .Setup(repo => repo.GetByAccountNumber(account.AccountNumber))
            .Returns(account);
        _accountsRepositoryMock
            .Setup(repo => repo.GetByAccountNumber(otherAccount.AccountNumber))
            .Returns(otherAccount);
        _clientController.Replenish(account.AccountNumber, "150");

        var expectedResult = new TransferResult.Success();
        TransferResult actualResult = _clientController.Transfer(account.AccountNumber, otherAccount.AccountNumber, "50");

        var expectedAccountFromResult = new Money(100m);
        Money actualAccountFromResult = account.Balance;

        var expectedAccountToResult = new Money(50m);
        Money actualAccountToResult = otherAccount.Balance;

        Assert.Equal(expectedResult, actualResult);
        Assert.Equal(expectedAccountFromResult, actualAccountFromResult);
        Assert.Equal(expectedAccountToResult, actualAccountToResult);
    }

    [Fact]

    public void ShouldNotTransferFromAccountToAccount_WhenClientTransferMoneyWithAmountGreaterThanOnAccount_ReturnsFailedTransferBalanceIsLowerThanTransferAmount()
    {
        Account account = _accountService.Create(_user);
        Account otherAccount = _accountService.Create(_user);
        _accountsRepositoryMock
            .Setup(repo => repo.GetById(account.Id))
            .Returns(account);
        _accountsRepositoryMock
            .Setup(repo => repo.GetById(otherAccount.Id))
            .Returns(otherAccount);
        _accountsRepositoryMock
            .Setup(repo => repo.GetByAccountNumber(account.AccountNumber))
            .Returns(account);
        _accountsRepositoryMock
            .Setup(repo => repo.GetByAccountNumber(otherAccount.AccountNumber))
            .Returns(otherAccount);

        var expectedResult = new TransferResult.BalanceIsLowerThanTransferAmount();
        TransferResult actualResult = _clientController.Transfer(account.AccountNumber, otherAccount.AccountNumber, "500");

        Assert.Equal(expectedResult, actualResult);
    }
}
