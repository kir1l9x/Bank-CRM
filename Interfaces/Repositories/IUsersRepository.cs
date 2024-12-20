using Entities.Users;

namespace Interfaces.Repositories;

public interface IUsersRepository
{
    User? GetByUsername(string username);

    User? GetById(Guid userId);

    IEnumerable<User> GetAll();

    void Add(User user);

    void Update(User user);

    void Delete(Guid userId);
}