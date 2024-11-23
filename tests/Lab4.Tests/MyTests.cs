using Itmo.ObjectOrientedProgramming.Lab4;
using Itmo.ObjectOrientedProgramming.Lab4.CommandCallers;
using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.SpecificCommands;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystemModes;
using Itmo.ObjectOrientedProgramming.Lab4.Loggers;
using Itmo.ObjectOrientedProgramming.Lab4.Parsers;
using Itmo.ObjectOrientedProgramming.Lab4.Readers;
using Itmo.ObjectOrientedProgramming.Lab4.SystemPaths;
using Itmo.ObjectOrientedProgramming.Lab4.SystemPaths.PathValidation;
using Itmo.ObjectOrientedProgramming.Lab4.Writers;
using Moq;
using Xunit;

namespace Lab4.Tests;

public class MyTests
{
    [Fact]

    public void ShouldReturnEquality_WnenCopyCommandCreatesByBuilderAndParser()
    {
        var readerMock = new Mock<IReader>();
        var writerMock = new Mock<IWriter>();
        var loggerMock = new Mock<ILogger>();
        var handlerMock = new Mock<ICommandHandler>();

        var tools = new ContextTools(readerMock.Object, writerMock.Object, loggerMock.Object);
        var parser = new ConsoleCommandParser(tools, handlerMock.Object);

        FileCopyCommand.FileCopyCommandBuilder builder = FileCopyCommand.Builder();
        string input = "file copy source.txt destination.txt";
        builder.AddSourcePath(new SystemPath(tools, "source.txt", new SystemPathValidation()));
        builder.AddDestinationPath(new SystemPath(tools, "destination.txt", new DirectoryPathValidation()));
        ICommand buildedCommand = builder.Build();
        handlerMock
            .Setup(h => h.Handle(It.IsAny<IEnumerator<string>>()))
            .Returns(buildedCommand);

        ICommand parsedCommand = parser.Parse(input);
        Assert.True(parsedCommand.Equals(buildedCommand));
    }

    [Fact]
    public void ShouldReturn_ConnectCommand()
    {
        var readerMock = new Mock<IReader>();
        var writerMock = new Mock<IWriter>();
        var loggerMock = new Mock<ILogger>();
        var handlerMock = new Mock<ICommandHandler>();

        var tools = new ContextTools(readerMock.Object, writerMock.Object, loggerMock.Object);
        var parser = new ConsoleCommandParser(tools, handlerMock.Object);

        ConnectCommand.ConnectCommandBuilder builder = ConnectCommand.Builder();
        string input = "connect /mnt/path -m local";
        builder.AddConnectionPath(new SystemPath(tools, "/mnt/path", new SystemPathValidation()));
        builder.AddConnectionMode(new FileSystemMode.Local());
        ICommand expectedCommand = builder.Build();
        handlerMock
            .Setup(h => h.Handle(It.IsAny<IEnumerator<string>>()))
            .Returns(expectedCommand);

        ICommand actualCommand = parser.Parse(input);
        Assert.True(actualCommand.Equals(expectedCommand));
    }

    [Fact]
    public void ShouldReturn_TreeGotoCommand()
    {
        var readerMock = new Mock<IReader>();
        var writerMock = new Mock<IWriter>();
        var loggerMock = new Mock<ILogger>();
        var handlerMock = new Mock<ICommandHandler>();

        var tools = new ContextTools(readerMock.Object, writerMock.Object, loggerMock.Object);
        var parser = new ConsoleCommandParser(tools, handlerMock.Object);

        TreeGotoCommand.TreeGotoCommandBuilder builder = TreeGotoCommand.Builder();
        string input = "tree goto /mnt/folder";
        builder.AddPathToGo(new SystemPath(tools, "/mnt/folder", new SystemPathValidation()));
        ICommand expectedCommand = builder.Build();
        handlerMock
            .Setup(h => h.Handle(It.IsAny<IEnumerator<string>>()))
            .Returns(expectedCommand);

        ICommand actualCommand = parser.Parse(input);
        Assert.True(actualCommand.Equals(expectedCommand));
    }

    [Fact]
    public void ShouldReturn_TreeListCommand()
    {
        var readerMock = new Mock<IReader>();
        var writerMock = new Mock<IWriter>();
        var loggerMock = new Mock<ILogger>();
        var handlerMock = new Mock<ICommandHandler>();

        var tools = new ContextTools(readerMock.Object, writerMock.Object, loggerMock.Object);
        var parser = new ConsoleCommandParser(tools, handlerMock.Object);

        TreeListCommand.TreeListCommandBuilder builder = TreeListCommand.Builder();
        string input = "tree list 3";
        builder.SetDepth(3);
        ICommand expectedCommand = builder.Build();
        handlerMock
            .Setup(h => h.Handle(It.IsAny<IEnumerator<string>>()))
            .Returns(expectedCommand);

        ICommand actualCommand = parser.Parse(input);
        Assert.True(actualCommand.Equals(expectedCommand));
    }

    [Fact]
    public void ShouldReturn_FileShowCommand()
    {
        var readerMock = new Mock<IReader>();
        var writerMock = new Mock<IWriter>();
        var loggerMock = new Mock<ILogger>();
        var handlerMock = new Mock<ICommandHandler>();

        var tools = new ContextTools(readerMock.Object, writerMock.Object, loggerMock.Object);
        var parser = new ConsoleCommandParser(tools, handlerMock.Object);

        FileShowCommand.FileShowCommandBuilder builder = FileShowCommand.Builder();
        string input = "file show example.txt -m console";
        builder.AddFilePath(new SystemPath(tools, "example.txt", new FilePathValidation()));
        ICommand expectedCommand = builder.Build();
        handlerMock
            .Setup(h => h.Handle(It.IsAny<IEnumerator<string>>()))
            .Returns(expectedCommand);

        ICommand actualCommand = parser.Parse(input);
        Assert.True(actualCommand.Equals(expectedCommand));
    }

    [Fact]
    public void ShouldReturn_DisconnectCommand()
    {
        var readerMock = new Mock<IReader>();
        var writerMock = new Mock<IWriter>();
        var loggerMock = new Mock<ILogger>();
        var handlerMock = new Mock<ICommandHandler>();

        var tools = new ContextTools(readerMock.Object, writerMock.Object, loggerMock.Object);
        var parser = new ConsoleCommandParser(tools, handlerMock.Object);

        var expectedCommand = new DisconnectCommand();
        handlerMock
            .Setup(h => h.Handle(It.IsAny<IEnumerator<string>>()))
            .Returns(expectedCommand);

        string input = "disconnect";
        ICommand actualCommand = parser.Parse(input);
        Assert.True(actualCommand.Equals(expectedCommand));
    }
}