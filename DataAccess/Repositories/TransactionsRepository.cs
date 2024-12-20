using Entities;
using Entities.Transactions;
using Interfaces.Repositories;
using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Npgsql;

namespace DataAccess.Repositories;

public class TransactionsRepository : ITransactionsRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public TransactionsRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public IEnumerable<Transaction> GetByAccountId(Guid accountId)
    {
        const string sql = $"""
                            SELECT id, account_id, amount, type, date
                            FROM transactions
                            WHERE account_id = :accountId;
                            """;

        using NpgsqlConnection connection = _connectionProvider.GetConnectionAsync(default).AsTask().GetAwaiter().GetResult();
        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("accountId", accountId);
        using NpgsqlDataReader reader = command.ExecuteReader();

        var transactions = new List<Transaction>();

        while (reader.Read())
        {
            transactions.Add(Transaction.Builder()
                .SetId(reader.GetGuid(0))
                .SetAccountId(reader.GetGuid(1))
                .SetAmount(new Money(reader.GetDecimal(2)))
                .SetType(reader.GetString(3))
                .SetDate(reader.GetDateTime(4))
                .Build());
        }

        return transactions;
    }

    public IEnumerable<Transaction> GetByDateRange(Guid accountId, DateTime startDate, DateTime endDate)
    {
        const string sql = $"""
                            SELECT id, account_id, amount, type, date
                            FROM transactions
                            WHERE account_id = :accountId
                              AND date BETWEEN :startDate AND :endDate;
                            """;

        using NpgsqlConnection connection = _connectionProvider.GetConnectionAsync(default).AsTask().GetAwaiter().GetResult();
        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("accountId", accountId)
            .AddParameter("startDate", startDate)
            .AddParameter("endDate", endDate);
        using NpgsqlDataReader reader = command.ExecuteReader();

        var transactions = new List<Transaction>();

        while (reader.Read())
        {
            transactions.Add(Transaction.Builder()
                .SetId(reader.GetGuid(0))
                .SetAccountId(reader.GetGuid(1))
                .SetAmount(new Money(reader.GetDecimal(2)))
                .SetType(reader.GetString(3))
                .SetDate(reader.GetDateTime(4))
                .Build());
        }

        return transactions;
    }

    public Transaction? GetById(Guid transactionId)
    {
        const string sql = $"""
                            SELECT id, account_id, amount, type, date
                            FROM transactions
                            WHERE id = :transactionId;
                            """;

        using NpgsqlConnection connection = _connectionProvider.GetConnectionAsync(default).AsTask().GetAwaiter().GetResult();
        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("transactionId", transactionId);
        using NpgsqlDataReader reader = command.ExecuteReader();

        if (!reader.Read())
            return null;

        return Transaction.Builder()
            .SetId(reader.GetGuid(0))
            .SetAccountId(reader.GetGuid(1))
            .SetAmount(new Money(reader.GetDecimal(2)))
            .SetType(reader.GetString(3))
            .SetDate(reader.GetDateTime(4))
            .Build();
    }

    public void Add(Transaction transaction)
    {
        const string sql = $"""
                            INSERT INTO transactions (id, account_id, amount, type, date)
                            VALUES (:id, :accountId, :amount, :type, :date);
                            """;

        using NpgsqlConnection connection = _connectionProvider.GetConnectionAsync(default).AsTask().GetAwaiter().GetResult();
        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("id", transaction.Id)
            .AddParameter("accountId", transaction.AccountId)
            .AddParameter("amount", transaction.Amount.Amount)
            .AddParameter("type", transaction.Type.ToString())
            .AddParameter("date", transaction.Date);

        command.ExecuteNonQuery();
    }

    public void Update(Transaction transaction)
    {
        const string sql = $"""
                            UPDATE transactions
                            SET account_id = :accountId,
                                amount = :amount,
                                type = :type,
                                date = :date
                            WHERE id = :id;
                            """;

        using NpgsqlConnection connection = _connectionProvider.GetConnectionAsync(default).AsTask().GetAwaiter().GetResult();
        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("id", transaction.Id)
            .AddParameter("accountId", transaction.AccountId)
            .AddParameter("amount", transaction.Amount.Amount)
            .AddParameter("type", transaction.Type.ToString())
            .AddParameter("date", transaction.Date);

        command.ExecuteNonQuery();
    }
}
