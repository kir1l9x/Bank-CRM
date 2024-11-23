using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.SpecificCommands;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.FileCommandsHandlers.FileChains.FileRenameCommandHandlers.RenameChain;

public class RenameNameHandler : BaseRenameChainElement
{
    public override ICommand? Handle(IEnumerator<string> commandRequest, FileRenameCommand.FileRenameCommandBuilder builder)
    {
        if (string.IsNullOrWhiteSpace(commandRequest.Current))
        {
            throw new ArgumentException("File name cannot be empty.");
        }

        builder.AddNewFileName(commandRequest.Current);

        return builder.Build();
    }
}