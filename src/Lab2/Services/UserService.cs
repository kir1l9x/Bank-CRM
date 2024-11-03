using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.Services;

public class UserService(ContextData data)
{
    public IUser CreateUser(string name)
    {
        IUser user = new UniversityUser(name);
        data.UsersRepository.Add(user);

        return user;
    }

    public UserResult GetUser(Guid userId)
    {
        return data.UsersRepository.GetById(userId);
    }
}