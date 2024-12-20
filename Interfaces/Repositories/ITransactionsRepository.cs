using Entities.Transactions;

namespace Interfaces.Repositories;

public interface ITransactionsRepository
{
    IEnumerable<Transaction> GetByAccountId(Guid accountId);

    IEnumerable<Transaction> GetByDateRange(Guid accountId, DateTime startDate, DateTime endDate);

    Transaction? GetById(Guid transactionId);

    void Add(Transaction transaction);

    void Update(Transaction transaction);
}