using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.SpecificCommands;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystemModes;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.ConnectCommandHandlers.ConnectChain;

public class ConnectLocalModeHandler : BaseConnectChainElement
{
    private const string Mode = "local";

    public override ICommand? Handle(IEnumerator<string> commandRequest, ConnectCommand.ConnectCommandBuilder builder)
    {
        if (commandRequest.Current.Equals(Mode, StringComparison.OrdinalIgnoreCase))
        {
            builder.AddConnectionMode(new FileSystemMode.Local());
            return builder.Build();
        }

        return null;
    }
}