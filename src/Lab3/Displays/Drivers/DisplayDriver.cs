using Itmo.ObjectOrientedProgramming.Lab3.Modifiers;

namespace Itmo.ObjectOrientedProgramming.Lab3.Displays.Drivers;

public class DisplayDriver(ColorModifier modifier)
{
    private ColorModifier _colorModifier = modifier;

    public void CleanUp()
    {
        Console.Clear();
    }

    public void ShowText(string text)
    {
        text = SetTextColor(text);
        Console.WriteLine(text);
    }

    public void ChangeModifier(ColorModifier modifier)
    {
        _colorModifier = modifier;
    }

    private string SetTextColor(string text)
    {
        return _colorModifier.Modify(text);
    }
}