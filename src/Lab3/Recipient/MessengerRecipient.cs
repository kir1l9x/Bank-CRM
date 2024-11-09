using Itmo.ObjectOrientedProgramming.Lab3.Messages;
using Itmo.ObjectOrientedProgramming.Lab3.Messenger;
using Itmo.ObjectOrientedProgramming.Lab3.Priorities;

namespace Itmo.ObjectOrientedProgramming.Lab3.Recipient;

public class MessengerRecipient(IMessenger messenger, PriorityLevel priorityLevel) : IRecipient
{
    public PriorityLevel PriorityLevel { get; } = priorityLevel;

    public RecipientDeliverResult Send(IMessage message)
    {
        if (message.PriorityLevel.PriorityNumber >= PriorityLevel.PriorityNumber)
        {
            messenger.ShowMessage(message.Render());
            return new RecipientDeliverResult.SuccessDelivered();
        }

        return new RecipientDeliverResult.NoOneGetMessage();
    }
}