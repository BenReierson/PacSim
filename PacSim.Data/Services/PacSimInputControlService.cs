using System;
using System.Diagnostics;
using PacSim.Models;
using PacSim.Services;

namespace PacSim.Services
{
    public class PacSimInputControlService : IPacSimInputControlService
    {
        readonly IPacSimMovementService pacSimMovement;

        public PacSimInputControlService(IPacSimMovementService pacSimMovement)
        {
            this.pacSimMovement = pacSimMovement;
        }

        public string? TryParseAndExecuteCommand(string input)
        {
            if (string.IsNullOrEmpty(input)) throw new ArgumentException("Input is empty");

            else if (input.StartsWith("PLACE", StringComparison.InvariantCulture))
            {
                var placeSplit = input.Split(' ', ',');
                if (placeSplit.Length == 4)
                {
                    var valid = int.TryParse(placeSplit[1], out var x);
                    valid &= int.TryParse(placeSplit[2], out var y);
                    valid &= Enum.TryParse<PacDirection>(placeSplit[3], out var facing);

                    if (valid) pacSimMovement.Place(x, y, facing);
                    else throw new ArgumentException("Invalid PLACE coordinates.");
                }
                else throw new ArgumentException($"Invalid PLACE coordinates.");
            }

            else if (input == "MOVE") pacSimMovement.Move();
            else if (input == "LEFT") pacSimMovement.Left();
            else if (input == "RIGHT") pacSimMovement.Right();
            else if (input == "REPORT") return pacSimMovement.Report()?.ToString();

            return null;
        }
    }
}
