using Entities.Accounts;
using Entities.Transactions;
using Entities.Users;

namespace Services.Accounts;

public interface IAccountService
{
    IEnumerable<Account> GetUsersAccounts(Guid userId);

    IEnumerable<Account> GetAll();

    Account? GetUserAccount(string accountNumber);

    Account Create(User user);

    void UpdateAccount(Guid accountId, Transaction transaction);

    void Delete(Account account);
}