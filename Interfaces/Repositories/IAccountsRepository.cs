using Entities.Accounts;

namespace Interfaces.Repositories;

public interface IAccountsRepository
{
    Account? GetByAccountNumber(string accountNumber);

    Account GetById(Guid accountId);

    IEnumerable<Account> GetByUserId(Guid userId);

    IEnumerable<Account> GetAll();

    bool CheckAccountExists(string accountNumber);

    Account Add(Account account);

    void Update(Account account);

    void Delete(Guid accountId);
}