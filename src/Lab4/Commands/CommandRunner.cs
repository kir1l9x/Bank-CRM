namespace Itmo.ObjectOrientedProgramming.Lab4.Commands;

public class CommandRunner
{
    private readonly Context _context;
    private ICommand? _command;

    public CommandRunner(Context context)
    {
        _context = context;
    }

    public void ReceiveCommand(ICommand command)
    {
        _command = command;
        Run();
        _command = null;
    }

    private void Run()
    {
        _command?.Execute(_context);
    }
}