using System.Diagnostics;
using PacSim.Models;

namespace PacSim.Services
{
    /// <summary>
    /// Represents a service that can place a 'Pac', change it's direction, move it, and report it's current position.
    /// This interface does not define the shape or dimensions of the Pac environment.
    /// </summary>
    public interface IPacSimMovementService
    {
        PacPosition Place(int x, int y, PacDirection facing);
        void Move();
        void Left();
        void Right();
        PacPosition? Report();
    }
}
