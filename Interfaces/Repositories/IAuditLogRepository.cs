using Entities.AuditLogs;

namespace Interfaces.Repositories;

public interface IAuditLogRepository
{
    IEnumerable<AuditLog> GetByUserId(Guid userId);

    IEnumerable<AuditLog> GetByDateRange(DateTime startDate, DateTime endDate);

    IEnumerable<AuditLog> GetAll();

    AuditLog? GetById(Guid auditLogId);

    void Add(AuditLog auditLog);
}