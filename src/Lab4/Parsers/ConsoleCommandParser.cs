using Itmo.ObjectOrientedProgramming.Lab4.CommandCallers;
using Itmo.ObjectOrientedProgramming.Lab4.Commands;

namespace Itmo.ObjectOrientedProgramming.Lab4.Parsers;

public class ConsoleCommandParser : ICommandParser
{
    private readonly ContextTools _tools;
    private ICommandHandler? _handler;

    public ConsoleCommandParser(ContextTools tools, ICommandHandler? handler)
    {
        _tools = tools;
        _handler = handler;
    }

    public ICommand Parse(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            throw new ArgumentException("Input command cannot be null or empty.", nameof(input));
        }

        var args = input.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();

        if (args.Count == 0)
        {
            throw new ArgumentException("No valid command detected.");
        }

        _handler = ChainCreator.CreateChain(_tools);

        List<string>.Enumerator enumerator = args.GetEnumerator();
        if (enumerator.MoveNext())
        {
            ICommand? command = _handler.Handle(enumerator);
            if (command == null)
            {
                throw new InvalidOperationException("Unsupported command");
            }

            return command;
        }

        throw new InvalidOperationException("Command parsing failed.");
    }

    public void StartCommandLoop(CommandRunner commandRunner)
    {
        while (true)
        {
            string? input = _tools.Reader.Read();
            if (input is null)
            {
                throw new Exception("Input could not be parsed.");
            }

            if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                break;

            try
            {
                ICommand command = Parse(input);
                commandRunner.ReceiveCommand(command);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}