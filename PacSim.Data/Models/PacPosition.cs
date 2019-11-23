using System;

namespace PacSim.Models
{
    /// <summary>
    /// Represents an immutable position of a 'Pac' in terms of X,Y coordinates and Facing direction.
    /// </summary>
    public readonly struct PacPosition : IEquatable<PacPosition>
    {
        public int X { get; }
        public int Y { get; }
        public PacDirection Facing { get; }

        internal PacPosition(int x, int y, PacDirection facing) => (X, Y, Facing) = (x, y, facing);

        public void Deconstruct(out int x, out int y, out PacDirection facing) => (x, y, facing) = (X, Y, Facing);

        public override string ToString() => $"{X},{Y},{Facing}";

        public bool Equals(PacPosition other) => X == other.X &&
                                                 Y == other.Y &&
                                                 Facing == other.Facing;

        public override bool Equals(object? obj) => obj is PacPosition position && Equals(position);

        public override int GetHashCode() => HashCode.Combine(X, Y, Facing);

        public static bool operator ==(PacPosition left, PacPosition right) => left.Equals(right);

        public static bool operator !=(PacPosition left, PacPosition right) => !(left == right);
    }

}
