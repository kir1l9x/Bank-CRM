using Itmo.ObjectOrientedProgramming.Lab4.Commands;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.FileCommandsHandlers.FileChains;

public interface IFileChainElement
{
    IFileChainElement AddNext(IFileChainElement fileChainElement);

    ICommand? Handle(IEnumerator<string> commandRequest);
}