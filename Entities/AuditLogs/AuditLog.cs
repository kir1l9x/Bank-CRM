namespace Entities.AuditLogs;

public class AuditLog
{
    public Guid Id { get; }

    public Guid? UserId { get; }

    public string ActionDescription { get; }

    public DateTime ActionTime { get; }

    public override string ToString()
    {
        return $"{Id}\n" +
               $"{UserId}\n" +
               $"{ActionDescription}\n" +
               $"{ActionTime:yyyy-MM-dd HH:mm:ss}";
    }

    public AuditLog(Guid? userId, string actionDescription)
    {
        Id = Guid.NewGuid();
        UserId = userId ?? Guid.Empty;
        ActionDescription = actionDescription;
        ActionTime = DateTime.Now;
    }

    public AuditLog(Guid id, Guid userId, string actionDescription, DateTime actionTime)
    {
        Id = id;
        UserId = userId;
        ActionDescription = actionDescription;
        ActionTime = actionTime;
    }
}