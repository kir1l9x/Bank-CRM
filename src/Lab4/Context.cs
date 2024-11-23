using Itmo.ObjectOrientedProgramming.Lab4.CommandCallers;
using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystems;
using Itmo.ObjectOrientedProgramming.Lab4.Loggers;
using Itmo.ObjectOrientedProgramming.Lab4.Loggers.FileServices;
using Itmo.ObjectOrientedProgramming.Lab4.Parsers;
using Itmo.ObjectOrientedProgramming.Lab4.Readers;
using Itmo.ObjectOrientedProgramming.Lab4.Writers;

namespace Itmo.ObjectOrientedProgramming.Lab4;

public class Context
{
    public IFileSystem? FileSystem { get; private set; }

    public ICommandParser CommandParser { get; private set; }

    public ICommandHandler? CommandHandler { get; private set; }

    public CommandRunner CommandStarter { get; private set; }

    public ContextTools Tools { get; private set; }

    public Context(IFileSystem fileSystem)
    {
        FileSystem = fileSystem;
        Tools = new ContextTools(new ConsoleReader(), new ConsoleWriter(), new Logger(new FileService()));
        CommandStarter = new CommandRunner(this);
        CommandParser = new ConsoleCommandParser(Tools, CommandHandler);
    }

    public void Run()
    {
        CommandParser.StartCommandLoop(CommandStarter);
    }
}