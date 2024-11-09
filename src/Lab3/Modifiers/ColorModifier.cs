using System.Drawing;

namespace Itmo.ObjectOrientedProgramming.Lab3.Modifiers;

public class ColorModifier(Color color) : IModifier
{
    public string Modify(string message)
    {
        return Crayon.Output.Rgb(color.R, color.G, color.B).Text(message);
    }
}