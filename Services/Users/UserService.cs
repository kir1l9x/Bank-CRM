using Entities.Users;
using Interfaces.Repositories;
using System.Security.Cryptography;
using System.Text;

namespace Services.Users;

public class UserService : IUserService
{
    private readonly IUsersRepository _usersRepository;
    private readonly CurrentUserService _currentUserService;

    public UserService(IUsersRepository usersRepository, CurrentUserService currentUserService)
    {
        _usersRepository = usersRepository;
        _currentUserService = currentUserService;
    }

    public User? GetUser(string username, string password)
    {
        User? user = _usersRepository.GetByUsername(username);
        if (user is null)
        {
            return null;
        }

        string hashedPassword = HashPassword(password);
        if (user.HashPassword != hashedPassword)
        {
            return null;
        }

        return user;
    }

    public User? Create(string username, string password)
    {
        User? userToFind = _usersRepository.GetByUsername(username);
        if (userToFind is not null)
        {
            return null;
        }

        string hashedPassword = HashPassword(password);
        int clientRoleNumber = 100;
        User user = User.Builder()
            .SetUsername(username)
            .SetHashPassword(hashedPassword)
            .SetRole(clientRoleNumber)
            .Build();
        _usersRepository.Add(user);

        return user;
    }

    public void ChangePassword(User user, string newPassword)
    {
        CheckForLegitimacy(user.Id);
        string hashedPassword = HashPassword(newPassword);
        user.ChangePassword(hashedPassword);
        _usersRepository.Update(user);
    }

    public void ChangeUserName(User user, string newUserName)
    {
        CheckForLegitimacy(user.Id);
        user.ChangeUserName(newUserName);
        _usersRepository.Update(user);
    }

    public void ChangeUserRole(User user, UserRole newUserRole)
    {
        CheckForAdminRights();
        user.ChangeRole(newUserRole);
    }

    public IEnumerable<User> GetAll()
    {
        CheckForAdminRights();
        return _usersRepository.GetAll();
    }

    public User? GetUserById(Guid userId)
    {
        CheckForAdminRights();
        return _usersRepository.GetById(userId);
    }

    public User? GetUserByName(string username)
    {
        User? user = _usersRepository.GetByUsername(username);
        return user;
    }

    public void DeleteUser(User user)
    {
        CheckForAdminRights();
        _usersRepository.Delete(user.Id);
    }

    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        byte[] bytes = Encoding.UTF8.GetBytes(password);
        byte[] hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }

    private void CheckForAdminRights()
    {
        if (_currentUserService.User is null)
        {
            throw new UnauthorizedAccessException("User is not authenticated");
        }

        if (_currentUserService.User.Role is not UserRole.Admin)
        {
            throw new UnauthorizedAccessException("User is not an admin");
        }
    }

    private void CheckForLegitimacy(Guid userWorkWithId)
    {
        User? currentUser = _currentUserService.User;
        if (currentUser is null)
        {
            throw new UnauthorizedAccessException("User is not authenticated");
        }

        if (currentUser.Id != userWorkWithId || currentUser.Role is not UserRole.Admin)
        {
            throw new UnauthorizedAccessException("You have not got access rights to update user's info");
        }
    }
}