namespace Itmo.ObjectOrientedProgramming.Lab4.TreeSignSettings;

public class TreeSignSetting
{
    public string DirectorySymbol { get; private set; } = "[DIR]";

    public string FileSymbol { get; private set; } = "[FILE]";

    public string Indent { get; private set; } = "  ";

    public string BranchSymbol { get; private set; } = "|-- ";

    public void SetDirectorySymbol(string symbol)
    {
        DirectorySymbol = symbol;
    }

    public void SetFileSymbol(string symbol)
    {
        FileSymbol = symbol;
    }

    public void SetIndent(string indent)
    {
        Indent = indent;
    }

    public void SetBranchSymbol(string symbol)
    {
        BranchSymbol = symbol;
    }
}