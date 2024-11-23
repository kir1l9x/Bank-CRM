using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.SpecificCommands;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.ConnectCommandHandlers.ConnectChain;

public interface IConnectChainElement
{
    IConnectChainElement AddNext(IConnectChainElement connectChainElement);

    ICommand? Handle(IEnumerator<string> commandRequest, ConnectCommand.ConnectCommandBuilder builder);
}