using Itmo.ObjectOrientedProgramming.Lab4.Commands;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandCallers;

public interface ICommandHandler
{
    ICommandHandler AddNext(ICommandHandler commandHandler);

    ICommand? Handle(IEnumerator<string> commandRequest);
}