using Itmo.ObjectOrientedProgramming.Lab3.Displays;
using Itmo.ObjectOrientedProgramming.Lab3.Messages;
using Itmo.ObjectOrientedProgramming.Lab3.Priorities;

namespace Itmo.ObjectOrientedProgramming.Lab3.Recipient;

public class DisplayRecipient(IDisplay display, PriorityLevel priorityLevel) : IRecipient
{
    public PriorityLevel PriorityLevel { get; } = priorityLevel;

    public RecipientDeliverResult Send(IMessage message)
    {
        if (message.PriorityLevel.PriorityNumber >= PriorityLevel.PriorityNumber)
        {
            display.Show(message.Render());
            return new RecipientDeliverResult.SuccessDelivered();
        }

        return new RecipientDeliverResult.NoOneGetMessage();
    }
}