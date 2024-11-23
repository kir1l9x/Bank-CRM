using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.SpecificCommands;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.TreeCommandsHandlers.TreeChain.TreeGoToCommandHandlers.GoToChain;

public abstract class BaseGoToChainElement : IGoToChainElement
{
    protected IGoToChainElement? Next { get; private set; }

    public IGoToChainElement AddNext(IGoToChainElement goToChainElement)
    {
        if (Next is null)
        {
            Next = goToChainElement;
        }
        else
        {
            Next.AddNext(goToChainElement);
        }

        return this;
    }

    public abstract ICommand? Handle(IEnumerator<string> commandRequest, TreeGotoCommand.TreeGotoCommandBuilder builder);
}