using Entities;
using Entities.Accounts;
using Entities.Transactions;
using Entities.Users;
using Interfaces.Repositories;
using Services.Users;

namespace Services.Transactions;

public class TransactionService : ITransactionService
{
    private readonly ITransactionsRepository _transactionsRepository;
    private readonly IAccountsRepository _accountsRepository;
    private readonly CurrentUserService _currentUserService;

    public TransactionService(
        ITransactionsRepository transactionsRepository,
        IAccountsRepository accountsRepository,
        CurrentUserService currentUserService)
    {
        _transactionsRepository = transactionsRepository;
        _accountsRepository = accountsRepository;
        _currentUserService = currentUserService;
    }

    public IEnumerable<Transaction> GetTransactionsByAccountId(Guid accountId)
    {
        CheckUserForLegitimacy(accountId);
        return _transactionsRepository.GetByAccountId(accountId);
    }

    public IEnumerable<Transaction> GetAccountTransactionsByDateRange(Guid accountId, DateTime startDate, DateTime endDate)
    {
        CheckUserForLegitimacy(accountId);
        return _transactionsRepository.GetByDateRange(accountId, startDate, endDate);
    }

    public Transaction Create(Guid accountId, Money amount, TransactionType transactionType)
    {
        Transaction transaction = Transaction.Builder().
            SetAccountId(accountId).
            SetAmount(amount).
            SetType(transactionType.ToString()).
            Build();

        _transactionsRepository.Add(transaction);
        return transaction;
    }

    private void CheckUserForLegitimacy(Guid accountId)
    {
        User? currentUser = _currentUserService.User;
        if (currentUser is null)
        {
            throw new UnauthorizedAccessException("User is not authenticated");
        }

        Account? requestAccount = _accountsRepository.GetById(accountId);
        if (requestAccount is null)
        {
            throw new UnauthorizedAccessException("Account does not exist");
        }

        if (requestAccount.UserId != currentUser.Id || currentUser.Role is not UserRole.Admin)
        {
            throw new UnauthorizedAccessException("You have not got access rights to check account's transactions");
        }
    }
}