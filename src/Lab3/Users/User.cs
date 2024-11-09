using Itmo.ObjectOrientedProgramming.Lab3.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.Users;

public class User(string name) : IUser
{
    private readonly List<ReadableMessage> _messages = [];

    public string Name { get; } = name;

    public IReadOnlyList<ReadableMessage> Messages => _messages;

    public void CollectMessage(IMessage message)
    {
        _messages.Add(new ReadableMessage(message));
    }

    public ReadMessageResult TryMarkAsRead(Guid messageId)
    {
        foreach (ReadableMessage curMessage in _messages)
        {
            if (curMessage.Id == messageId)
            {
                return curMessage.MarkAsRead();
            }
        }

        return new ReadMessageResult.AttemptToReadByNonOwner();
    }
}