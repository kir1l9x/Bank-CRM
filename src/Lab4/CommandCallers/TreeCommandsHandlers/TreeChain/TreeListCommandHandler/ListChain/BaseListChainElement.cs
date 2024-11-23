using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.SpecificCommands;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.TreeCommandsHandlers.TreeChain.TreeListCommandHandler.ListChain;

public abstract class BaseListChainElement : IListChainElement
{
    protected IListChainElement? Next { get; private set; }

    public IListChainElement AddNext(IListChainElement listChainElement)
    {
        if (Next is null)
        {
            Next = listChainElement;
        }
        else
        {
            Next.AddNext(listChainElement);
        }

        return this;
    }

    public abstract ICommand? Handle(IEnumerator<string> commandRequest, TreeListCommand.TreeListCommandBuilder builder);
}