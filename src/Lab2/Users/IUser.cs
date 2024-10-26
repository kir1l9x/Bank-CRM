namespace Itmo.ObjectOrientedProgramming.Lab2.Users;

public interface IUser
{
    string Name { get; }

    Guid Id { get; }
}