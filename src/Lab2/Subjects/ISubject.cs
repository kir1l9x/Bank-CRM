using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.Subjects;

public interface ISubject
{
    Guid Id { get; }

    string Name { get; }

    IUser Owner { get; }

    ISubject Clone(IUser user);
}