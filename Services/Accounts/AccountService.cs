using Entities;
using Entities.Accounts;
using Entities.Transactions;
using Entities.Users;
using Interfaces.Repositories;
using Services.Users;
using System.Security.Cryptography;
using System.Text;

namespace Services.Accounts;

public class AccountService : IAccountService
{
    private readonly IAccountsRepository _accountsRepository;
    private readonly CurrentUserService _currentUserService;

    public AccountService(
        IAccountsRepository accountsRepository,
        CurrentUserService currentUserService)
    {
        _accountsRepository = accountsRepository;
        _currentUserService = currentUserService;
    }

    public IEnumerable<Account> GetUsersAccountsByAdmin(Guid userId)
    {
        CheckForAdminRights();
        return _accountsRepository.GetByUserId(userId);
    }

    public IEnumerable<Account> GetUsersAccounts(Guid userId)
    {
        return _accountsRepository.GetByUserId(userId);
    }

    public IEnumerable<Account> GetAll()
    {
        CheckForAdminRights();
        return _accountsRepository.GetAll();
    }

    public Account? GetUserAccount(string accountNumber)
    {
        return _accountsRepository.GetByAccountNumber(accountNumber);
    }

    public Account Create(User user)
    {
        string accountNumber = GenerateAccountNumber();
        while (_accountsRepository.CheckAccountExists(accountNumber))
        {
            accountNumber = GenerateAccountNumber();
        }

        Account account = Account.Builder()
            .SetAccountNumber(accountNumber)
            .SetUserId(user.Id)
            .Build();

        _accountsRepository.Add(account);

        return account;
    }

    public void UpdateAccount(Guid accountId, Transaction transaction)
    {
        Account account = _accountsRepository.GetById(accountId);
        Money resultMoney = account.Balance;
        switch (transaction.Type)
        {
            case TransactionType.Replenishment:
                resultMoney += transaction.Amount;
                break;
            case TransactionType.WriteOff:
                resultMoney -= transaction.Amount;
                break;
        }

        account.UpdateMoney(resultMoney);
        _accountsRepository.Update(account);
    }

    public void Delete(Account account)
    {
        CheckForAdminRights();
        _accountsRepository.Delete(account.Id);
    }

    private void CheckForAdminRights()
    {
        if (_currentUserService.User is null)
        {
            throw new UnauthorizedAccessException("User is not authenticated");
        }

        if (_currentUserService.User.Role is not UserRole.Admin)
        {
            throw new UnauthorizedAccessException("You must have admin's rights");
        }
    }

    private string GenerateAccountNumber()
    {
        const int length = 20;
        const string digits = "0123456789";

        var result = new StringBuilder(length);
        using (var rng = RandomNumberGenerator.Create())
        {
            byte[] buffer = new byte[length];
            rng.GetBytes(buffer);

            foreach (byte b in buffer)
            {
                result.Append(digits[b % digits.Length]);
            }
        }

        return result.ToString();
    }
}