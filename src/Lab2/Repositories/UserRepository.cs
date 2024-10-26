using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.Repositories;

public class UserRepository
{
    private readonly List<IUser> _users = [];

    public void Add(IUser entity)
    {
        _users.Add(entity);
    }

    public UserResult GetById(Guid id)
    {
        foreach (IUser user in _users)
        {
            if (user.Id == id)
            {
                return new UserResult.SuccessFound(user);
            }
        }

        return new UserResult.FailureFound();
    }
}