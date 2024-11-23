using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.SpecificCommands;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.ConnectCommandHandlers.ConnectChain;

public class ConnectFlagHandler : BaseConnectChainElement
{
    private const string Flag = "-m";
    private readonly ContextTools _contextTools;

    public ConnectFlagHandler(ContextTools contextTools)
    {
        _contextTools = contextTools;
    }

    public override ICommand? Handle(IEnumerator<string> commandRequest, ConnectCommand.ConnectCommandBuilder builder)
    {
        if (commandRequest.Current.Equals(Flag, StringComparison.OrdinalIgnoreCase))
        {
            if (!commandRequest.MoveNext())
            {
                _contextTools.Writer.Write("Connect command must have a flag");
                return null;
            }
        }

        return Next?.Handle(commandRequest, builder);
    }
}