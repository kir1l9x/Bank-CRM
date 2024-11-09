using Itmo.ObjectOrientedProgramming.Lab3.Loggers;
using Itmo.ObjectOrientedProgramming.Lab3.Loggers.LogLevels;
using Itmo.ObjectOrientedProgramming.Lab3.Messages;
using Itmo.ObjectOrientedProgramming.Lab3.Priorities;

namespace Itmo.ObjectOrientedProgramming.Lab3.Recipient;

public class RecipientLogger(ILogger logger, IRecipient recipient) : IRecipient
{
    public PriorityLevel PriorityLevel { get; } = recipient.PriorityLevel;

    public RecipientDeliverResult Send(IMessage message)
    {
        RecipientDeliverResult result = recipient.Send(message);
        if (result is RecipientDeliverResult.SuccessDelivered)
        {
            logger.Log($"Messages {message.Id} was successfully sent from the recipient", LogLevel.Info);
            return result;
        }

        logger.Log($"No one get the message {message.Id} from recipient", LogLevel.Warning);

        return result;
    }
}