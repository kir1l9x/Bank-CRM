using Itmo.ObjectOrientedProgramming.Lab4.Commands;

namespace Itmo.ObjectOrientedProgramming.Lab4.Parsers;

public interface ICommandParser
{
    ICommand Parse(string input);

    void StartCommandLoop(CommandRunner commandRunner);
}