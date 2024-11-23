using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.SpecificCommands;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.FileCommandsHandlers.FileChains.FileCopyCommandHandlers.CopyChain;

public interface ICopyChainElement
{
    ICopyChainElement AddNext(ICopyChainElement copyChainElement);

    ICommand? Handle(IEnumerator<string> commandRequest, FileCopyCommand.FileCopyCommandBuilder builder);
}