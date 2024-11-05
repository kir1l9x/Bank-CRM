using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.Repositories.SpecificRepositories;

public class UserRepository : BaseRepository<IUser, UserResult>
{
    public override UserResult GetById(Guid id)
    {
        foreach (IUser user in Items)
        {
            if (user.Id == id)
            {
                return new UserResult.SuccessFound(user);
            }
        }

        return new UserResult.FailureFound();
    }
}