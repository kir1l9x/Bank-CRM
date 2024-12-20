using Entities.Users;

namespace Services.Users;

public class CurrentUserService : ICurrentUserService
{
    public User? User { get; set; }
}