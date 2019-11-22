using System.Diagnostics;
using PacSim.Models;

namespace PacSim.Services
{
    public interface IPacSimMovementService
    {
        PacPosition Place(int x, int y, PacDirection facing);
        void Move();
        void Left();
        void Right();
        PacPosition? Report();
    }
}
