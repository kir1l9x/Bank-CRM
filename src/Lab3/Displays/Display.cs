using Itmo.ObjectOrientedProgramming.Lab3.Displays.Drivers;

namespace Itmo.ObjectOrientedProgramming.Lab3.Displays;

public class Display(IDriver driver) : IDisplay
{
    public void Show(string text)
    {
        driver.CleanUp();
        driver.ShowText(text);
    }
}