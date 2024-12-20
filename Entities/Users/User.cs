namespace Entities.Users;

public class User
{
    public Guid Id { get; }

    public string Username { get; private set; }

    public string HashPassword { get; private set; }

    public UserRole Role { get; private set; }

    public DateTime CreatedOn { get; private set; }

    private User(Guid id, string username, string hashPassword, UserRole role, DateTime createdOn)
    {
        Id = id;
        Username = username;
        HashPassword = hashPassword;
        Role = role;
        CreatedOn = createdOn;
    }

    public static UserBuilder Builder()
    {
        return new UserBuilder();
    }

    public void ChangePassword(string newPassword)
    {
        HashPassword = newPassword;
    }

    public void ChangeUserName(string newUsername)
    {
        Username = newUsername;
    }

    public void ChangeRole(UserRole newRole)
    {
        Role = newRole;
    }

    public override string ToString()
    {
        return $"{Id}\n" +
               $"{Username}\n" +
               $"{Role}\n" +
               $"{CreatedOn}";
    }

    public class UserBuilder
    {
        private Guid? _id;
        private string? _username;
        private string? _hashPassword;
        private UserRole? _role;
        private DateTime? _createdOn;

        public UserBuilder SetId(Guid? id)
        {
            _id = id;
            return this;
        }

        public UserBuilder SetUsername(string username)
        {
            _username = username;
            return this;
        }

        public UserBuilder SetHashPassword(string hashPassword)
        {
            _hashPassword = hashPassword;
            return this;
        }

        public UserBuilder SetRole(int roleId)
        {
            switch (roleId)
            {
                case 52:
                    _role = new UserRole.Admin();
                    break;
                case 100:
                    _role = new UserRole.Client();
                    break;
            }

            return this;
        }

        public UserBuilder SetCreatedOn(DateTime createdOn)
        {
            _createdOn = createdOn;
            return this;
        }

        public User Build()
        {
            return new User(
                _id ?? Guid.NewGuid(),
                _username ?? throw new NullReferenceException(),
                _hashPassword ?? throw new NullReferenceException(),
                _role ?? throw new NullReferenceException(),
                _createdOn ?? DateTime.Now);
        }
    }
}