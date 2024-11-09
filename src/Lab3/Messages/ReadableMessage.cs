using Itmo.ObjectOrientedProgramming.Lab3.Priorities;

namespace Itmo.ObjectOrientedProgramming.Lab3.Messages;

public class ReadableMessage(IMessage message) : IMessage
{
    public Guid Id { get; } = message.Id;

    public PriorityLevel PriorityLevel => message.PriorityLevel;

    public bool IsRead { get; private set; }

    public string Render()
    {
        return message.Render();
    }

    public ReadMessageResult MarkAsRead()
    {
        if (!IsRead)
        {
            IsRead = true;
            return new ReadMessageResult.MessageWasRead();
        }

        return new ReadMessageResult.MessageIsAlreadyRead();
    }
}