using Entities.Users;
using Interfaces.Repositories;
using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Npgsql;

namespace DataAccess.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public UsersRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public User? GetByUsername(string username)
    {
        const string sql = $"""
                            SELECT id, username, hash_password, role, created_on
                            FROM users
                            WHERE username = :username;
                            """;

        using NpgsqlConnection connection = _connectionProvider.GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();
        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("username", username);
        using NpgsqlDataReader reader = command.ExecuteReader();

        if (!reader.Read())
            return null;

        return User.Builder()
            .SetId(reader.GetGuid(0))
            .SetUsername(reader.GetString(1))
            .SetHashPassword(reader.GetString(2))
            .SetRole(reader.GetInt32(3))
            .SetCreatedOn(reader.GetDateTime(4))
            .Build();
    }

    public User? GetById(Guid userId)
    {
        const string sql = $"""
                            SELECT id, username, hash_password, role, created_on
                            FROM users
                            WHERE id = :userId;
                            """;

        using NpgsqlConnection connection = _connectionProvider.GetConnectionAsync(default).AsTask().GetAwaiter().GetResult();
        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("userId", userId);
        using NpgsqlDataReader reader = command.ExecuteReader();

        if (!reader.Read())
            return null;

        return User.Builder()
            .SetId(reader.GetGuid(0))
            .SetUsername(reader.GetString(1))
            .SetHashPassword(reader.GetString(2))
            .SetRole(reader.GetInt32(3))
            .SetCreatedOn(reader.GetDateTime(4))
            .Build();
    }

    public IEnumerable<User> GetAll()
    {
        const string sql = $"""
                            SELECT id, username, hash_password, role, created_on
                            FROM users;
                            """;

        using NpgsqlConnection connection = _connectionProvider.GetConnectionAsync(default).AsTask().GetAwaiter().GetResult();
        using var command = new NpgsqlCommand(sql, connection);
        using NpgsqlDataReader reader = command.ExecuteReader();

        var users = new List<User>();

        while (reader.Read())
        {
            users.Add(User.Builder()
                .SetId(reader.GetGuid(0))
                .SetUsername(reader.GetString(1))
                .SetHashPassword(reader.GetString(2))
                .SetRole(reader.GetInt32(3))
                .SetCreatedOn(reader.GetDateTime(4))
                .Build());
        }

        return users;
    }

    public void Add(User user)
    {
        const string sql = $"""
                            INSERT INTO users (id, username, hash_password, role, created_on)
                            VALUES (:id, :username, :hashPassword, :role, :createdOn);
                            """;

        using NpgsqlConnection connection = _connectionProvider.GetConnectionAsync(default).AsTask().GetAwaiter().GetResult();
        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("id", user.Id)
            .AddParameter("username", user.Username)
            .AddParameter("hashPassword", user.HashPassword)
            .AddParameter("role", user.Role.RoleId)
            .AddParameter("createdOn", user.CreatedOn);

        command.ExecuteNonQuery();
    }

    public void Update(User user)
    {
        const string sql = $"""
                            UPDATE users
                            SET username = :username,
                                hash_password = :hashPassword,
                                role = :role
                            WHERE id = :id;
                            """;

        using NpgsqlConnection connection = _connectionProvider.GetConnectionAsync(default).AsTask().GetAwaiter().GetResult();
        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("id", user.Id)
            .AddParameter("username", user.Username)
            .AddParameter("hashPassword", user.HashPassword)
            .AddParameter("role", user.Role.RoleId);

        command.ExecuteNonQuery();
    }

    public void Delete(Guid userId)
    {
        const string sql = $"""
                            DELETE FROM users
                            WHERE id = :userId;
                            """;

        using NpgsqlConnection connection = _connectionProvider.GetConnectionAsync(default).AsTask().GetAwaiter().GetResult();
        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("userId", userId);

        command.ExecuteNonQuery();
    }
}
