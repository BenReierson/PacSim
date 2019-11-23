using System;

namespace PacSim.Models
{
    /// <summary>
    /// Represents an immutable position of a 'Pac' in terms of X,Y coordinates and Facing direction.
    /// </summary>
    public readonly struct PacPosition : IEquatable<PacPosition>
    {
        public readonly int X { get; }
        public readonly int Y { get; }
        public readonly PacDirection Facing { get; }

        internal PacPosition(int x, int y, PacDirection facing) => (X, Y, Facing) = (x, y, facing);

        public readonly void Deconstruct(out int x, out int y, out PacDirection facing) => (x, y, facing) = (X, Y, Facing);

        public readonly override string ToString() => $"{X},{Y},{Facing}";

        public readonly bool Equals(PacPosition other) => X == other.X &&
                                                 Y == other.Y &&
                                                 Facing == other.Facing;

        public readonly override bool Equals(object? obj) => obj is PacPosition position && Equals(position);

        public readonly override int GetHashCode() => HashCode.Combine(X, Y, Facing);

        public static bool operator ==(PacPosition left, PacPosition right) => left.Equals(right);

        public static bool operator !=(PacPosition left, PacPosition right) => !(left == right);
    }

}
