using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.SpecificCommands;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.ConnectCommandHandlers.ConnectChain;

public abstract class BaseConnectChainElement : IConnectChainElement
{
    protected IConnectChainElement? Next { get; private set; }

    public IConnectChainElement AddNext(IConnectChainElement connectChainElement)
    {
        if (Next is null)
        {
            Next = connectChainElement;
        }
        else
        {
            Next.AddNext(connectChainElement);
        }

        return this;
    }

    public abstract ICommand? Handle(IEnumerator<string> commandRequest, ConnectCommand.ConnectCommandBuilder builder);
}