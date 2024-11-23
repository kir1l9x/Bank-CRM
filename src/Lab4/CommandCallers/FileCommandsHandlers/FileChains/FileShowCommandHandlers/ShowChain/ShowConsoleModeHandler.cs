using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.SpecificCommands;
using Itmo.ObjectOrientedProgramming.Lab4.ShowModes;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.FileCommandsHandlers.FileChains.FileShowCommandHandlers.ShowChain;

public class ShowConsoleModeHandler : BaseShowChainElement
{
    private const string Mode = "console";

    public override ICommand? Handle(IEnumerator<string> commandRequest, FileShowCommand.FileShowCommandBuilder builder)
    {
        if (commandRequest.Current.Equals(Mode, StringComparison.OrdinalIgnoreCase))
        {
            builder.AddShowMode(new ShowMode.ConsoleMode());
            return builder.Build();
        }

        return null;
    }
}