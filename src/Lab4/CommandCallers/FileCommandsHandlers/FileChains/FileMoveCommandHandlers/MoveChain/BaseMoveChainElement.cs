using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.SpecificCommands;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.FileCommandsHandlers.FileChains.FileMoveCommandHandlers.MoveChain;

public abstract class BaseMoveChainElement : IMoveChainElement
{
    protected IMoveChainElement? Next { get; private set; }

    public IMoveChainElement AddNext(IMoveChainElement moveChainElement)
    {
        if (Next is null)
        {
            Next = moveChainElement;
        }
        else
        {
            Next.AddNext(moveChainElement);
        }

        return this;
    }

    public abstract ICommand? Handle(IEnumerator<string> commandRequest, FileMoveCommand.FileMoveCommandBuilder builder);
}