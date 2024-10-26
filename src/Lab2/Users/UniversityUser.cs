using Itmo.ObjectOrientedProgramming.Lab2.Ensures;

namespace Itmo.ObjectOrientedProgramming.Lab2.Users;

public class UniversityUser : IUser
{
    public Guid Id { get; }

    public string Name { get; }

    public UniversityUser(string name)
    {
        Ensure.NotEmpty(name, nameof(name));

        Id = Guid.NewGuid();
        Name = name;
    }
}