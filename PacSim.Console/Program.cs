using System;
using System.Diagnostics;
using PacSim.Services;

namespace PacSim.ConsoleApp
{
    /// <summary>
    /// This app will read commands via console input and execute in a simulation of a Pacman on a 5x5 grid.
    /// A valid PLACE command must be executed before any other commands are valid.
    ///
    /// Valid commands:
    /// PLACE X,Y,F - will put the Pacman on the grid in positon X,Y and facing NORTH,SOUTH, EAST or WEST.
    /// MOVE - will move Pacman one unit forward in the direction it is currently facing.
    /// LEFT - will rotate Pacman 90 degrees in the specified direction without changing the position of Pacman.
    /// RIGHT - will rotate Pacman 90 degrees in the specified direction without changing the position of Pacman.
    /// REPORT - will output Pacman's current position in the grid via the console.
    /// 
    /// All invalid or unrecognised commands will be ignored.
    /// 
    /// </summary>
    class PacSimConsoleApp
    {
        static IPacSimInputControlService pacSim;

        static void Main(string[] args)
        {
            //Create the input controller and grid services.
            pacSim = new PacSimInputControlService(new PacSimGridMovementService(5,5));

            Console.WriteLine("5x5 grid created. Waiting for PLACE command.");

            while (true)
            {
                try
                {
                    //interpret each line read from the console
                    var result = pacSim.ParseCommand(Console.ReadLine())?.Invoke();

                    //display output of command if any
                    if (result != null) Console.WriteLine(result);
                }
                catch (Exception ex)
                {//All exceptions or otherwise invalid input is ignored
                    Debug.WriteLine(ex.Message);
                }
            }
        }
    }
}
