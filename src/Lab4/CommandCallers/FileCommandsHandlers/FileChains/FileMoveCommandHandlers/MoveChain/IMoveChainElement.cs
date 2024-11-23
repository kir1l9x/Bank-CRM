using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.SpecificCommands;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.FileCommandsHandlers.FileChains.FileMoveCommandHandlers.MoveChain;

public interface IMoveChainElement
{
    IMoveChainElement AddNext(IMoveChainElement moveChainElement);

    ICommand? Handle(IEnumerator<string> commandRequest, FileMoveCommand.FileMoveCommandBuilder builder);
}