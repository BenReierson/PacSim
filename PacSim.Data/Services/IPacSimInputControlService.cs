namespace PacSim.Services
{
    public interface IPacSimInputControlService
    {
        string? TryParseAndExecuteCommand(string input);
    }
}