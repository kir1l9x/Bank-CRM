using System.Diagnostics.CodeAnalysis;

namespace ConsoleUI;

public interface IScenarioProvider
{
    bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario);
}