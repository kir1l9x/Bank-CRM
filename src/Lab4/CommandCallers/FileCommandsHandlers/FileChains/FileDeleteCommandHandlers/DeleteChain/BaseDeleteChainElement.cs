using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.SpecificCommands;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.FileCommandsHandlers.FileChains.FileDeleteCommandHandlers.DeleteChain;

public abstract class BaseDeleteChainElement : IDeleteChainElement
{
    protected IDeleteChainElement? Next { get; private set; }

    public IDeleteChainElement AddNext(IDeleteChainElement deleteChainElement)
    {
        if (Next is null)
        {
            Next = deleteChainElement;
        }
        else
        {
            Next.AddNext(deleteChainElement);
        }

        return this;
    }

    public abstract ICommand? Handle(IEnumerator<string> commandRequest, FileDeleteCommand.FileDeleteCommandBuilder builder);
}