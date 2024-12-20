using Entities.AuditLogs;

namespace Services.AuditLogs;

public interface IAuditLogService
{
    void Create(Guid? userId, string logInfo);

    IEnumerable<AuditLog> GetAuditLogsByUserId(Guid userId);

    IEnumerable<AuditLog> GetAll();

    IEnumerable<AuditLog> GetAuditLogsByDateRange(DateTime startDate, DateTime endDate);

    AuditLog? GetAuditLogById(Guid id);
}