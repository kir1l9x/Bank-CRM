using Entities;
using Entities.Accounts;
using Interfaces.Repositories;
using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Npgsql;

namespace DataAccess.Repositories;

public class AccountsRepository : IAccountsRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public AccountsRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public Account? GetByAccountNumber(string accountNumber)
    {
        const string sql = $"""
                            SELECT id, account_number, user_id, balance
                            FROM accounts
                            WHERE account_number = :accountNumber;
                            """;

        NpgsqlConnection connection = _connectionProvider.GetConnectionAsync(default).AsTask().GetAwaiter().GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("accountNumber", accountNumber);

        using NpgsqlDataReader reader = command.ExecuteReader();

        if (!reader.Read())
            return null;

        return Account.Builder()
            .SetId(reader.GetGuid(0))
            .SetAccountNumber(reader.GetString(1))
            .SetUserId(reader.GetGuid(2))
            .SetBalance(new Money(reader.GetDecimal(3)))
            .Build();
    }

    public Account GetById(Guid accountId)
    {
        const string sql = $"""
                            SELECT id, account_number, user_id, balance
                            FROM accounts
                            WHERE id = :accountId;
                            """;

        NpgsqlConnection connection = _connectionProvider.GetConnectionAsync(default).AsTask().GetAwaiter().GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("accountId", accountId);

        using NpgsqlDataReader reader = command.ExecuteReader();

        if (!reader.Read())
            throw new KeyNotFoundException($"Account with ID {accountId} not found.");

        return Account.Builder()
            .SetId(reader.GetGuid(0))
            .SetAccountNumber(reader.GetString(1))
            .SetUserId(reader.GetGuid(2))
            .SetBalance(new Money(reader.GetDecimal(3)))
            .Build();
    }

    public IEnumerable<Account> GetByUserId(Guid userId)
    {
        const string sql = $"""
                            SELECT id, account_number, user_id, balance
                            FROM accounts
                            WHERE user_id = :userId;
                            """;

        NpgsqlConnection connection = _connectionProvider.GetConnectionAsync(default).AsTask().GetAwaiter().GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("userId", userId);

        using NpgsqlDataReader reader = command.ExecuteReader();

        var accounts = new List<Account>();

        while (reader.Read())
        {
            accounts.Add(Account.Builder()
                .SetId(reader.GetGuid(0))
                .SetAccountNumber(reader.GetString(1))
                .SetUserId(reader.GetGuid(2))
                .SetBalance(new Money(reader.GetDecimal(3)))
                .Build());
        }

        return accounts;
    }

    public IEnumerable<Account> GetAll()
    {
        const string sql = $"""
                            SELECT id, account_number, user_id, balance
                            FROM accounts;
                            """;

        NpgsqlConnection connection = _connectionProvider.GetConnectionAsync(default).AsTask().GetAwaiter().GetResult();

        using var command = new NpgsqlCommand(sql, connection);
        using NpgsqlDataReader reader = command.ExecuteReader();

        var accounts = new List<Account>();

        while (reader.Read())
        {
            accounts.Add(Account.Builder()
                .SetId(reader.GetGuid(0))
                .SetAccountNumber(reader.GetString(1))
                .SetUserId(reader.GetGuid(2))
                .SetBalance(new Money(reader.GetDecimal(3)))
                .Build());
        }

        return accounts;
    }

    public bool CheckAccountExists(string accountNumber)
    {
        const string sql = $"""
                            SELECT COUNT(1)
                            FROM accounts
                            WHERE account_number = :accountNumber;
                            """;

        NpgsqlConnection connection = _connectionProvider.GetConnectionAsync(default).AsTask().GetAwaiter().GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("accountNumber", accountNumber);

        return Convert.ToBoolean(command.ExecuteScalar());
    }

    public Account Add(Account account)
    {
        const string sql = $"""
                            INSERT INTO accounts (id, account_number, user_id, balance)
                            VALUES (:id, :accountNumber, :userId, :balance)
                            RETURNING id, account_number, user_id, balance;
                            """;

        NpgsqlConnection connection = _connectionProvider.GetConnectionAsync(default).AsTask().GetAwaiter().GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("id", account.Id)
            .AddParameter("accountNumber", account.AccountNumber)
            .AddParameter("userId", account.UserId)
            .AddParameter("balance", account.Balance.Amount);

        using NpgsqlDataReader reader = command.ExecuteReader();

        if (!reader.Read())
            throw new InvalidOperationException("Failed to insert account.");

        return Account.Builder()
            .SetId(reader.GetGuid(0))
            .SetAccountNumber(reader.GetString(1))
            .SetUserId(reader.GetGuid(2))
            .SetBalance(new Money(reader.GetDecimal(3)))
            .Build();
    }

    public void Update(Account account)
    {
        const string sql = $"""
                            UPDATE accounts
                            SET account_number = :accountNumber,
                                user_id = :userId,
                                balance = :balance
                            WHERE id = :id;
                            """;

        NpgsqlConnection connection = _connectionProvider.GetConnectionAsync(default).AsTask().GetAwaiter().GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("id", account.Id)
            .AddParameter("accountNumber", account.AccountNumber)
            .AddParameter("userId", account.UserId)
            .AddParameter("balance", account.Balance.Amount);

        command.ExecuteNonQuery();
    }

    public void Delete(Guid accountId)
    {
        const string sql = """
        DELETE FROM accounts
        WHERE id = :accountId;
        """;

        NpgsqlConnection connection = _connectionProvider.GetConnectionAsync(default).AsTask().GetAwaiter().GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("accountId", accountId);

        command.ExecuteNonQuery();
    }
}
