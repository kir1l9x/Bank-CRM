using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.SpecificCommands;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.FileCommandsHandlers.FileChains.FileCopyCommandHandlers.CopyChain;

public abstract class BaseCopyChainElement : ICopyChainElement
{
    protected ICopyChainElement? Next { get; private set; }

    public ICopyChainElement AddNext(ICopyChainElement copyChainElement)
    {
        if (Next is null)
        {
            Next = copyChainElement;
        }
        else
        {
            Next.AddNext(copyChainElement);
        }

        return this;
    }

    public abstract ICommand? Handle(IEnumerator<string> commandRequest, FileCopyCommand.FileCopyCommandBuilder builder);
}