using Entities;
using Entities.Transactions;

namespace Services.Transactions;

public interface ITransactionService
{
    IEnumerable<Transaction> GetTransactionsByAccountId(Guid accountId);

    IEnumerable<Transaction> GetAccountTransactionsByDateRange(Guid accountId, DateTime startDate, DateTime endDate);

    Transaction Create(Guid accountId, Money amount, TransactionType transactionType);
}