using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.SpecificCommands;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.FileCommandsHandlers.FileChains.FileShowCommandHandlers.ShowChain;

public abstract class BaseShowChainElement : IShowChainElement
{
    protected IShowChainElement? Next { get; private set; }

    public IShowChainElement AddNext(IShowChainElement showChainElement)
    {
        if (Next is null)
        {
            Next = showChainElement;
        }
        else
        {
            Next.AddNext(showChainElement);
        }

        return this;
    }

    public abstract ICommand? Handle(IEnumerator<string> commandRequest, FileShowCommand.FileShowCommandBuilder builder);
}