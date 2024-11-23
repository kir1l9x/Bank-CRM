using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.SpecificCommands;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.FileCommandsHandlers.FileChains.FileRenameCommandHandlers.RenameChain;

public abstract class BaseRenameChainElement : IRenameChainElement
{
    protected IRenameChainElement? Next { get; private set; }

    public IRenameChainElement AddNext(IRenameChainElement renameChainElement)
    {
        if (Next is null)
        {
            Next = renameChainElement;
        }
        else
        {
            Next.AddNext(renameChainElement);
        }

        return this;
    }

    public abstract ICommand? Handle(IEnumerator<string> commandRequest, FileRenameCommand.FileRenameCommandBuilder builder);
}