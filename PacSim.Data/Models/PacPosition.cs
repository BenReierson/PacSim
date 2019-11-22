using System;

namespace PacSim.Models
{
    public readonly struct PacPosition : IEquatable<PacPosition>
    {
        public int X { get; }
        public int Y { get; }
        public PacDirection Facing { get; }

        internal PacPosition(int x, int y, PacDirection facing) => (X, Y, Facing) = (x, y, facing);

        public void Deconstruct(out int x, out int y, out PacDirection facing) => (x, y, facing) = (X, Y, Facing);

        public override string ToString()
        {
            return $"{X},{Y},{Facing}";
        }

        public override bool Equals(object? obj)
        {
            return obj is PacPosition position && Equals(position);
        }

        public bool Equals(PacPosition other)
        {
            return X == other.X &&
                   Y == other.Y &&
                   Facing == other.Facing;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Facing);
        }

        public static bool operator ==(PacPosition left, PacPosition right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(PacPosition left, PacPosition right)
        {
            return !(left == right);
        }
    }

}
