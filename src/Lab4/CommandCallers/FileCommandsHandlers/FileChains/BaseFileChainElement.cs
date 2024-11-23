using Itmo.ObjectOrientedProgramming.Lab4.Commands;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.FileCommandsHandlers.FileChains;

public abstract class BaseFileChainElement : IFileChainElement
{
    protected IFileChainElement? Next { get; private set; }

    public IFileChainElement AddNext(IFileChainElement fileChainElement)
    {
        if (Next is null)
        {
            Next = fileChainElement;
        }
        else
        {
            Next.AddNext(fileChainElement);
        }

        return this;
    }

    public abstract ICommand? Handle(IEnumerator<string> commandRequest);
}