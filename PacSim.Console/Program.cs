using System;
using System.Diagnostics;
using PacSim.Services;

namespace PacSim.ConsoleApp
{
    class Program
    {
        static IPacSimInputControlService pacSim;

        static void Main(string[] args)
        {
            pacSim = new PacSimInputControlService(new PacSimGridMovementService(5,5));

            Console.WriteLine("5x5 Grid Created. Please PLACE Pacman");

            while (true)
            {
                try
                {
                    var result = pacSim.TryParseAndExecuteCommand(Console.ReadLine());
                    if (result != null) Console.WriteLine(result);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }
    }
}
