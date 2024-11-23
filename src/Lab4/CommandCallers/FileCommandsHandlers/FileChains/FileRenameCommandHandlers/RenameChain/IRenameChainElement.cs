using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.SpecificCommands;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.FileCommandsHandlers.FileChains.FileRenameCommandHandlers.RenameChain;

public interface IRenameChainElement
{
    IRenameChainElement AddNext(IRenameChainElement renameChainElement);

    ICommand? Handle(IEnumerator<string> commandRequest, FileRenameCommand.FileRenameCommandBuilder builder);
}