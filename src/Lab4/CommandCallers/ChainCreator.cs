using Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.ConnectCommandHandlers;
using Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.ConnectCommandHandlers.ConnectChain;
using Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.DisconnectCommandHandlers;
using Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.FileCommandsHandlers;
using Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.FileCommandsHandlers.FileChains.FileCopyCommandHandlers;
using Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.FileCommandsHandlers.FileChains.FileCopyCommandHandlers.CopyChain;
using Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.FileCommandsHandlers.FileChains.FileDeleteCommandHandlers;
using Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.FileCommandsHandlers.FileChains.FileDeleteCommandHandlers.DeleteChain;
using Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.FileCommandsHandlers.FileChains.FileMoveCommandHandlers;
using Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.FileCommandsHandlers.FileChains.FileMoveCommandHandlers.MoveChain;
using Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.FileCommandsHandlers.FileChains.FileRenameCommandHandlers;
using Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.FileCommandsHandlers.FileChains.FileRenameCommandHandlers.RenameChain;
using Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.FileCommandsHandlers.FileChains.FileShowCommandHandlers;
using Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.FileCommandsHandlers.FileChains.FileShowCommandHandlers.ShowChain;
using Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.TreeCommandsHandlers;
using Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.TreeCommandsHandlers.TreeChain.TreeGoToCommandHandlers;
using Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.TreeCommandsHandlers.TreeChain.TreeGoToCommandHandlers.GoToChain;
using Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.TreeCommandsHandlers.TreeChain.TreeListCommandHandler;
using Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.TreeCommandsHandlers.TreeChain.TreeListCommandHandler.ListChain;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandCallers;

public static class ChainCreator
{
    public static ICommandHandler CreateChain(ContextTools contextTools)
    {
        var addressConnector = new ConnectAddressHandler(contextTools);
        var connectFlag = new ConnectFlagHandler(contextTools);
        var connectMode = new ConnectLocalModeHandler();
        addressConnector.AddNext(connectFlag).AddNext(connectMode);

        var copySourcePath = new CopySourcePathHandler(contextTools);
        var copyDestinationPath = new CopyDestinationPathHandler(contextTools);
        copySourcePath.AddNext(copyDestinationPath);

        var deletePath = new DeletePathHandler(contextTools);

        var moveSourcePath = new MoveSourcePathHandler(contextTools);
        var moveDestinationPath = new MoveDestinationPathHandler(contextTools);
        moveSourcePath.AddNext(moveDestinationPath);

        var renamePath = new RenamePathHandler(contextTools);
        var renameName = new RenameNameHandler();
        renamePath.AddNext(renameName);

        var showPath = new ShowPathHandler(contextTools);
        var showFlag = new ShowFlagHandler(contextTools);
        var showMode = new ShowConsoleModeHandler();
        showPath.AddNext(showFlag).AddNext(showMode);

        var gotoPath = new GoToPathHandler(contextTools);

        var listFlag = new ListFlagHandler(contextTools);
        var listDepth = new ListDepthHandler();
        listFlag.AddNext(listDepth);

        var fileCopy = new FileCopyHandler(copySourcePath, contextTools);
        var fileMove = new FileMoveHandler(moveSourcePath, contextTools);
        var fileRename = new FileRenameHandler(renamePath, contextTools);
        var fileShow = new FileShowHandler(showPath, contextTools);
        var fileDelete = new FileDeleteHandler(deletePath, contextTools);
        fileCopy.AddNext(fileMove).AddNext(fileRename).AddNext(fileShow).AddNext(fileDelete);

        var connect = new ConnectCommandHandler(addressConnector, contextTools);
        var disconnect = new DisconnectHandler();

        var treeGoto = new TreeGoToHandler(gotoPath, contextTools);
        var treeList = new TreeListHandler(listFlag, contextTools);
        treeGoto.AddNext(treeList);

        var file = new FileHandler(fileCopy, contextTools);
        var tree = new TreeCommandHandler(treeGoto, contextTools);

        return connect.AddNext(disconnect).AddNext(file).AddNext(tree);
    }
}