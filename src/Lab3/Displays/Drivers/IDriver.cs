namespace Itmo.ObjectOrientedProgramming.Lab3.Displays.Drivers;

public interface IDriver
{
    void CleanUp();

    void ShowText(string text);
}