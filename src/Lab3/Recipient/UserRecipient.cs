using Itmo.ObjectOrientedProgramming.Lab3.Messages;
using Itmo.ObjectOrientedProgramming.Lab3.Priorities;
using Itmo.ObjectOrientedProgramming.Lab3.Users;

namespace Itmo.ObjectOrientedProgramming.Lab3.Recipient;

public class UserRecipient(IUser user, PriorityLevel priorityLevel) : IRecipient
{
    public PriorityLevel PriorityLevel { get; } = priorityLevel;

    public RecipientDeliverResult Send(IMessage message)
    {
        if (message.PriorityLevel.PriorityNumber >= PriorityLevel.PriorityNumber)
        {
            user.CollectMessage(message);
            return new RecipientDeliverResult.SuccessDelivered();
        }

        return new RecipientDeliverResult.NoOneGetMessage();
    }
}