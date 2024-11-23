using Itmo.ObjectOrientedProgramming.Lab4.Commands;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandCallers;

public abstract class CommandHandlerBase : ICommandHandler
{
    protected ICommandHandler? Next { get; private set; }

    public ICommandHandler AddNext(ICommandHandler commandHandler)
    {
        if (Next is null)
        {
            Next = commandHandler;
        }
        else
        {
            Next.AddNext(commandHandler);
        }

        return this;
    }

    public abstract ICommand? Handle(IEnumerator<string> commandRequest);
}