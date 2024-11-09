using Itmo.ObjectOrientedProgramming.Lab3.Priorities;

namespace Itmo.ObjectOrientedProgramming.Lab3.Messages;

public interface IMessage
{
    Guid Id { get; }

    PriorityLevel PriorityLevel { get; }

    string Render();
}