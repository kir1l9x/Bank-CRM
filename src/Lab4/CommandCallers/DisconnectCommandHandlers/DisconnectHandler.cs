using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.SpecificCommands;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.DisconnectCommandHandlers;

public class DisconnectHandler : CommandHandlerBase
{
    private const string CommandName = "disconnect";

    public override ICommand? Handle(IEnumerator<string> commandRequest)
    {
        if (commandRequest.Current.Equals(CommandName, StringComparison.OrdinalIgnoreCase))
        {
            return new DisconnectCommand();
        }

        return Next?.Handle(commandRequest);
    }
}