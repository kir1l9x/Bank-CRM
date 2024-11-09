namespace Itmo.ObjectOrientedProgramming.Lab3.Recipient;

public abstract record RecipientDeliverResult
{
    public sealed record SuccessDelivered : RecipientDeliverResult;

    public sealed record NoOneGetMessage : RecipientDeliverResult;
}