namespace Itmo.ObjectOrientedProgramming.Lab3.Topics;

public abstract record TopicDeliverResult
{
    public sealed record SuccessDelivered : TopicDeliverResult;

    public sealed record NoOneGetMessage : TopicDeliverResult;
}