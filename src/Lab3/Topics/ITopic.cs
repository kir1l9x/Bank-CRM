using Itmo.ObjectOrientedProgramming.Lab3.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.Topics;

public interface ITopic
{
    string Name { get; }

    IMessage Message { get; }

    public TopicDeliverResult SendToRecipient();
}