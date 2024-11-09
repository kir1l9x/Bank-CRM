namespace Itmo.ObjectOrientedProgramming.Lab3.Messages;

public abstract record ReadMessageResult
{
    public sealed record MessageIsAlreadyRead : ReadMessageResult;

    public sealed record MessageWasRead : ReadMessageResult;

    public sealed record AttemptToReadByNonOwner : ReadMessageResult;
}