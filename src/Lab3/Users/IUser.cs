using Itmo.ObjectOrientedProgramming.Lab3.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.Users;

public interface IUser
{
    string Name { get; }

    void CollectMessage(IMessage message);
}