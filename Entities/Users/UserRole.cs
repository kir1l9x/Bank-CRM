namespace Entities.Users;

public abstract record UserRole
{
    public string RoleName { get; init; }

    public int RoleId { get; init; }

    private UserRole(string roleName, int roleId)
    {
        RoleName = roleName;
        RoleId = roleId;
    }

    public sealed record Admin() : UserRole("Admin", 52);

    public sealed record Client() : UserRole("Client", 100);
}