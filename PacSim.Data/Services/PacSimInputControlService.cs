using System;
using System.Diagnostics;
using PacSim.Models;
using PacSim.Services;

namespace PacSim.Services
{
    /// <summary>
    /// Represents an implementation of IPacSimInputControlService that executes commands against a given IPacSimMovementService.
    /// </summary>
    public class PacSimInputControlService : IPacSimInputControlService
    {
        readonly IPacSimMovementService pacSimMovement;

        public PacSimInputControlService(IPacSimMovementService pacSimMovement)
        {
            this.pacSimMovement = pacSimMovement;
        }

        /// <summary>
        /// Attempts to parse input string as a single command and executes it if valid.
        /// 
        /// Valid command mapping:
        /// PLACE X,Y,F - executes IPacSimMovementService.Place(x,y,f)
        /// MOVE - executes IPacSimMovementService.Move()
        /// LEFT - executes IPacSimMovementService.Left()
        /// RIGHT - executes IPacSimMovementService.Right()
        /// REPORT - executes IPacSimMovementService.Report()
        /// 
        /// Throws ArgumentException on invalid or unrecognised commands.
        /// </summary>
        /// <param name="input">Single valid command</param>
        /// <returns>any output returned after command execution, null if none</returns>
        public string? TryParseAndExecuteCommand(string input)
        {
            if (string.IsNullOrEmpty(input)) throw new ArgumentException("Input is empty");
            else if (input.StartsWith("PLACE", StringComparison.InvariantCulture))
            {
                // This looks to be a PLACE command. Attempt to split it to find x,y,f arguments
                var placeSplit = input.Split(' ', ',');

                if (placeSplit.Length == 4)
                {//Corrent number of arguments found, attempt to parse them

                    var valid = int.TryParse(placeSplit[1], out var x);
                    valid &= int.TryParse(placeSplit[2], out var y);
                    valid &= Enum.TryParse<PacDirection>(placeSplit[3], out var facing);

                    if (valid) return pacSimMovement.Place(x, y, facing).ToString();
                    else throw new ArgumentException("Invalid PLACE coordinates.");
                }
                else throw new ArgumentException($"Invalid PLACE coordinates.");
            }
            else if (input == "MOVE") pacSimMovement.Move();
            else if (input == "LEFT") pacSimMovement.Left();
            else if (input == "RIGHT") pacSimMovement.Right();
            else if (input == "REPORT") return pacSimMovement.Report()?.ToString();
            else throw new ArgumentException(input);

            return null;
        }
    }
}
