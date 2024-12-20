using Entities.Users;

namespace Services.Users;

public interface ICurrentUserService
{
    User? User { get; set; }
}