namespace PacSim.Services
{
    /// <summary>
    /// Represents a service that can parse string inputs and execute them, returning any result.
    /// </summary>
    public interface IPacSimInputControlService
    {
        string? TryParseAndExecuteCommand(string input);
    }
}