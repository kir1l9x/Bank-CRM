using Itmo.ObjectOrientedProgramming.Lab3.Messages;
using Itmo.ObjectOrientedProgramming.Lab3.Priorities;

namespace Itmo.ObjectOrientedProgramming.Lab3.Recipient;

public class GroupRecipient(IList<IRecipient> recipients, PriorityLevel priorityLevel) : IRecipient
{
    private readonly List<IRecipient> _recipients = recipients.ToList();

    public PriorityLevel PriorityLevel { get; } = priorityLevel;

    public RecipientDeliverResult Send(IMessage message)
    {
        bool wasDelivered = false;
        foreach (IRecipient recipient in _recipients)
        {
            RecipientDeliverResult currentResult = recipient.Send(message);
            if (currentResult is RecipientDeliverResult.SuccessDelivered)
            {
                wasDelivered = true;
            }
        }

        return wasDelivered ?
            new RecipientDeliverResult.SuccessDelivered() :
            new RecipientDeliverResult.NoOneGetMessage();
    }
}