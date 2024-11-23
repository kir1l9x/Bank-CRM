using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.SpecificCommands;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.TreeCommandsHandlers.TreeChain.TreeListCommandHandler.ListChain;

public class ListDepthHandler : BaseListChainElement
{
    public override ICommand? Handle(IEnumerator<string> commandRequest, TreeListCommand.TreeListCommandBuilder builder)
    {
        int depth = int.Parse(commandRequest.Current);
        if (depth <= 0)
        {
            throw new ArgumentException("Depth must be greater than 0.");
        }

        builder.SetDepth(depth);

        return builder.Build();
    }
}