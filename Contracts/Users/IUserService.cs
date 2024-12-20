using Entities.Users;

namespace Services.Users;

public interface IUserService
{
    User? GetUser(string username, string password);

    User? Create(string username, string password);

    void ChangePassword(User user, string newPassword);

    void ChangeUserName(User user, string newUserName);

    void ChangeUserRole(User user, UserRole newUserRole);

    IEnumerable<User> GetAll();

    User? GetUserById(Guid userId);

    User? GetUserByName(string username);

    void DeleteUser(User user);
}