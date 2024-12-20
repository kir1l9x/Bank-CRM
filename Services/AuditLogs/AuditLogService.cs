using Entities.AuditLogs;
using Entities.Users;
using Interfaces.Repositories;
using Services.Users;

namespace Services.AuditLogs;

public class AuditLogService : IAuditLogService
{
    private readonly IAuditLogRepository _auditLogRepository;
    private readonly CurrentUserService _currentUserService;

    public AuditLogService(IAuditLogRepository auditLogRepository, CurrentUserService currentUserService)
    {
        _auditLogRepository = auditLogRepository;
        _currentUserService = currentUserService;
    }

    public void Create(Guid? userId, string logInfo)
    {
        var auditLog = new AuditLog(userId, logInfo);
        _auditLogRepository.Add(auditLog);
    }

    public IEnumerable<AuditLog> GetAuditLogsByUserId(Guid userId)
    {
        CheckForAdminRights();
        return _auditLogRepository.GetByUserId(userId);
    }

    public IEnumerable<AuditLog> GetAll()
    {
        CheckForAdminRights();
        return _auditLogRepository.GetAll();
    }

    public IEnumerable<AuditLog> GetAuditLogsByDateRange(DateTime startDate, DateTime endDate)
    {
        CheckForAdminRights();
        return _auditLogRepository.GetByDateRange(startDate, endDate);
    }

    public AuditLog? GetAuditLogById(Guid id)
    {
        CheckForAdminRights();
        return _auditLogRepository.GetById(id);
    }

    private void CheckForAdminRights()
    {
        if (_currentUserService.User is null)
        {
            _auditLogRepository.Add(new AuditLog(Guid.Empty, "User is not authenticated"));
            throw new UnauthorizedAccessException("User is not authenticated");
        }

        if (_currentUserService.User.Role is not UserRole.Admin)
        {
            _auditLogRepository.Add(new AuditLog(_currentUserService.User.Id, "User is not owner or admin to check account transactions"));
            throw new UnauthorizedAccessException("You have not got access rights to check account's transactions");
        }
    }
}