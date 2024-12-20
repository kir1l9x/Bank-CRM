using Entities.AuditLogs;
using Interfaces.Repositories;
using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Npgsql;

namespace DataAccess.Repositories;

public class AuditLogRepository : IAuditLogRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public AuditLogRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public IEnumerable<AuditLog> GetByUserId(Guid userId)
    {
        const string sql = """
        SELECT id, user_id, action_description, action_time
        FROM audit_logs
        WHERE user_id = :userId;
        """;

        NpgsqlConnection connection = _connectionProvider.GetConnectionAsync(default).AsTask().GetAwaiter().GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("userId", userId);

        using NpgsqlDataReader reader = command.ExecuteReader();

        var logs = new List<AuditLog>();

        while (reader.Read())
        {
            logs.Add(new AuditLog(
                id: reader.GetGuid(0),
                userId: reader.GetGuid(1),
                actionDescription: reader.GetString(2),
                actionTime: reader.GetDateTime(3)));
        }

        return logs;
    }

    public IEnumerable<AuditLog> GetByDateRange(DateTime startDate, DateTime endDate)
    {
        const string sql = """
        SELECT id, user_id, action_description, action_time
        FROM audit_logs
        WHERE action_time BETWEEN :startDate AND :endDate;
        """;

        NpgsqlConnection connection = _connectionProvider.GetConnectionAsync(default).AsTask().GetAwaiter().GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("startDate", startDate)
            .AddParameter("endDate", endDate);

        using NpgsqlDataReader reader = command.ExecuteReader();

        var logs = new List<AuditLog>();

        while (reader.Read())
        {
            logs.Add(new AuditLog(
                id: reader.GetGuid(0),
                userId: reader.GetGuid(1),
                actionDescription: reader.GetString(2),
                actionTime: reader.GetDateTime(3)));
        }

        return logs;
    }

    public IEnumerable<AuditLog> GetAll()
    {
        const string sql = """
        SELECT id, user_id, action_description, action_time
        FROM audit_logs;
        """;

        NpgsqlConnection connection = _connectionProvider.GetConnectionAsync(default).AsTask().GetAwaiter().GetResult();

        using var command = new NpgsqlCommand(sql, connection);
        using NpgsqlDataReader reader = command.ExecuteReader();

        var logs = new List<AuditLog>();

        while (reader.Read())
        {
            logs.Add(new AuditLog(
                id: reader.GetGuid(0),
                userId: reader.GetGuid(1),
                actionDescription: reader.GetString(2),
                actionTime: reader.GetDateTime(3)));
        }

        return logs;
    }

    public AuditLog? GetById(Guid auditLogId)
    {
        const string sql = $"""
                            SELECT id, user_id, action_description, action_time
                            FROM audit_logs
                            WHERE id = :auditLogId;
                            """;

        NpgsqlConnection connection = _connectionProvider.GetConnectionAsync(default).AsTask().GetAwaiter().GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("auditLogId", auditLogId);

        using NpgsqlDataReader reader = command.ExecuteReader();

        if (!reader.Read())
            return null;

        return new AuditLog(
            id: reader.GetGuid(0),
            userId: reader.GetGuid(1),
            actionDescription: reader.GetString(2),
            actionTime: reader.GetDateTime(3));
    }

    public void Add(AuditLog auditLog)
    {
        const string sql = $"""
                            INSERT INTO audit_logs (id, user_id, action_description, action_time)
                            VALUES (:id, :userId, :actionDescription, :actionTime);
                            """;

        NpgsqlConnection connection = _connectionProvider.GetConnectionAsync(default).AsTask().GetAwaiter().GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("id", auditLog.Id)
            .AddParameter("userId", auditLog.UserId)
            .AddParameter("actionDescription", auditLog.ActionDescription)
            .AddParameter("actionTime", auditLog.ActionTime);

        command.ExecuteNonQuery();
    }
}
