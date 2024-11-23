using Itmo.ObjectOrientedProgramming.Lab4.Loggers;
using Itmo.ObjectOrientedProgramming.Lab4.Readers;
using Itmo.ObjectOrientedProgramming.Lab4.Writers;

namespace Itmo.ObjectOrientedProgramming.Lab4;

public class ContextTools
{
    public IReader Reader { get; }

    public IWriter Writer { get; }

    public ILogger Logger { get; }

    public ContextTools(IReader reader, IWriter writer, ILogger logger)
    {
        Reader = reader;
        Writer = writer;
        Logger = logger;
    }
}