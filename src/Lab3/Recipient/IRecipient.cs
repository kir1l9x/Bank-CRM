using Itmo.ObjectOrientedProgramming.Lab3.Messages;
using Itmo.ObjectOrientedProgramming.Lab3.Priorities;

namespace Itmo.ObjectOrientedProgramming.Lab3.Recipient;

public interface IRecipient
{
    PriorityLevel PriorityLevel { get; }

    RecipientDeliverResult Send(IMessage message);
}