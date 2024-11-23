using Itmo.ObjectOrientedProgramming.Lab4.Commands;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.TreeCommandsHandlers.TreeChain;

public interface ITreeChainElement
{
    ITreeChainElement AddNext(ITreeChainElement treeChainElement);

    ICommand? Handle(IEnumerator<string> commandRequest);
}