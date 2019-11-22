using System.ComponentModel;

namespace PacSim.Models
{
    public enum PacDirection
    {
        NORTH,
        SOUTH,
        EAST,
        WEST
    }

    public static class PacDirectionExtensions
    {
        public static PacDirection ToTheLeft(this PacDirection currentDirection) => currentDirection switch
        {
            PacDirection.NORTH => PacDirection.WEST,
            PacDirection.SOUTH => PacDirection.EAST,
            PacDirection.EAST => PacDirection.NORTH,
            PacDirection.WEST => PacDirection.SOUTH,
            _ => throw new InvalidEnumArgumentException()
        };

        public static PacDirection ToTheRight(this PacDirection currentDirection) => currentDirection switch
        {
            PacDirection.NORTH => PacDirection.EAST,
            PacDirection.SOUTH => PacDirection.WEST,
            PacDirection.EAST => PacDirection.SOUTH,
            PacDirection.WEST => PacDirection.NORTH,
            _ => throw new InvalidEnumArgumentException()
        };
    }
}
