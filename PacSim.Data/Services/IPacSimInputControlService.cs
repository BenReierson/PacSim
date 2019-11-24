using System;

namespace PacSim.Services
{
    /// <summary>
    /// Represents a service that can parse string inputs and execute them, returning any result.
    /// </summary>
    public interface IPacSimInputControlService
    {
        /// <summary>
        /// Attempts to parse input string as a single command.
        /// Throws ArgumentException on invalid or unrecognised commands.
        /// </summary>
        /// <param name="input">Potential input string to parse as command</param>
        /// <returns>A function that, when invoked, will execute the command and return any output</returns>
        Func<string?> ParseCommand(string input);
    }
}