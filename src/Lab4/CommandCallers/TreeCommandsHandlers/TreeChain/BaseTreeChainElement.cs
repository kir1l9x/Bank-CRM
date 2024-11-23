using Itmo.ObjectOrientedProgramming.Lab4.Commands;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.TreeCommandsHandlers.TreeChain;

public abstract class BaseTreeChainElement : ITreeChainElement
{
    protected ITreeChainElement? Next { get; private set; }

    public ITreeChainElement AddNext(ITreeChainElement treeChainElement)
    {
        if (Next is null)
        {
            Next = treeChainElement;
        }
        else
        {
            Next.AddNext(treeChainElement);
        }

        return this;
    }

    public abstract ICommand? Handle(IEnumerator<string> commandRequest);
}