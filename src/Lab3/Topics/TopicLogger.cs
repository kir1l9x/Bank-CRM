using Itmo.ObjectOrientedProgramming.Lab3.Loggers;
using Itmo.ObjectOrientedProgramming.Lab3.Loggers.LogLevels;
using Itmo.ObjectOrientedProgramming.Lab3.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.Topics;

public class TopicLogger(ILogger logger, ITopic topic) : ITopic
{
    public string Name { get; } = topic.Name;

    public IMessage Message { get; } = topic.Message;

    public TopicDeliverResult SendToRecipient()
    {
        logger.Log($"Messages {Message.Id} was successfully sent from the topic", LogLevel.Info);
        TopicDeliverResult result = topic.SendToRecipient();
        if (result is TopicDeliverResult.SuccessDelivered)
        {
            return result;
        }

        logger.Log($"No one recipient get the message {Message.Id} from topic", LogLevel.Warning);

        return result;
    }
}