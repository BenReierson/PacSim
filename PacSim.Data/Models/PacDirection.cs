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
        /// <summary>
        /// Translates the current direction into the direction (of the four defined) that is 'to the left'.
        /// Can also be thought of as 90 degree turn counter-clockwise.
        /// </summary>
        public static PacDirection ToTheLeft(this PacDirection currentDirection) => currentDirection switch
        {
            PacDirection.NORTH => PacDirection.WEST,
            PacDirection.SOUTH => PacDirection.EAST,
            PacDirection.EAST => PacDirection.NORTH,
            PacDirection.WEST => PacDirection.SOUTH,
            _ => throw new InvalidEnumArgumentException()
        };

        /// <summary>
        /// Translates the current direction into the direction (of the four defined) that is 'to the right'.
        /// Can also be thought of as 90 degree turn clockwise.
        /// </summary>
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
