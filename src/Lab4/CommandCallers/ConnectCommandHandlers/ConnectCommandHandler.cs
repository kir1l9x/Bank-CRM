using Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.ConnectCommandHandlers.ConnectChain;
using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.SpecificCommands;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.ConnectCommandHandlers;

public class ConnectCommandHandler : CommandHandlerBase
{
    private const string CommandName = "connect";
    private readonly IConnectChainElement _next;
    private readonly ContextTools _contextTools;

    public ConnectCommandHandler(IConnectChainElement next, ContextTools contextTools)
    {
        _next = next;
        _contextTools = contextTools;
    }

    public override ICommand? Handle(IEnumerator<string> commandRequest)
    {
        if (!commandRequest.Current.Equals(CommandName, StringComparison.OrdinalIgnoreCase))
        {
            return Next?.Handle(commandRequest);
        }

        if (!commandRequest.MoveNext())
        {
            _contextTools.Writer.Write("Connect command must have a path to connecting directory");
            return null;
        }

        return _next.Handle(commandRequest, ConnectCommand.Builder());
    }
}