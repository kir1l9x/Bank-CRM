using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.SpecificCommands;
using Itmo.ObjectOrientedProgramming.Lab4.SystemPaths;
using Itmo.ObjectOrientedProgramming.Lab4.SystemPaths.PathValidation;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.ConnectCommandHandlers.ConnectChain;

public class ConnectAddressHandler : BaseConnectChainElement
{
    private readonly ContextTools _contextTools;

    public ConnectAddressHandler(ContextTools contextTools)
    {
        _contextTools = contextTools;
    }

    public override ICommand? Handle(IEnumerator<string> commandRequest, ConnectCommand.ConnectCommandBuilder builder)
    {
        builder.AddConnectionPath(new SystemPath(_contextTools, commandRequest.Current, new DirectoryPathValidation()));

        if (!commandRequest.MoveNext())
        {
            _contextTools.Writer.Write("Connect command must have a flag");
            return null;
        }

        return Next?.Handle(commandRequest, builder);
    }
}