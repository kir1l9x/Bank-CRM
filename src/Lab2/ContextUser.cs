using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2;

public class ContextUser
{
    public IUser User { get; set; }

    public ContextUser(IUser user)
    {
        User = user;
    }
}