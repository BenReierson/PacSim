using System;
using System.Diagnostics;
using PacSim.Models;

namespace PacSim.Services
{
    public class PacSimGridMovementService : IPacSimMovementService
    {
        const string NOT_PLACED_MESSAGE = "Pac has no current position. You must call PLACE first.";

        public int GridWidth { get; }
        public int GridHeight { get; }

        PacPosition? currentPosition;
        public PacPosition? CurrentPosition
        {
            get => currentPosition;
            set
            {
                currentPosition = value;
                Debug.WriteLine(currentPosition);
            }
        }

        public PacSimGridMovementService(int gridWidth, int gridHeight)
        {
            GridHeight = gridHeight;
            GridWidth = gridWidth;
        }

        PacPosition GetValidPositionInGrid(int newX, int newY, PacDirection newFacing) => new PacPosition(newX, newY, newFacing) switch
        {
            var (x, _, _) when x < 0 || x >= GridWidth => throw new ArgumentOutOfRangeException($"Invalid X. Valid values are 0-{GridWidth}"),
            var (_, y, _) when y < 0 || y >= GridHeight => throw new ArgumentOutOfRangeException($"Invalid Y. Valid values are 0-{GridHeight}"),
            var newPosition => newPosition
        };

        #region IPacSimMovementService
        public PacPosition Place(int x, int y, PacDirection facing) =>
            (CurrentPosition = GetValidPositionInGrid(x, y, facing)) ?? throw new InvalidOperationException();

        public void Left()
        {
            if (CurrentPosition is null) throw new InvalidOperationException(NOT_PLACED_MESSAGE);

            var actualPos = CurrentPosition.Value;

            CurrentPosition = GetValidPositionInGrid(actualPos.X, actualPos.Y, actualPos.Facing.ToTheLeft());
        }

        public void Right()
        {
            if (CurrentPosition is null) throw new InvalidOperationException(NOT_PLACED_MESSAGE);

            var actualPos = CurrentPosition.Value;

            CurrentPosition = GetValidPositionInGrid(actualPos.X, actualPos.Y, actualPos.Facing.ToTheRight());
        }

        public void Move()
        {
            if (CurrentPosition is null) throw new InvalidOperationException(NOT_PLACED_MESSAGE);

            var actualPos = CurrentPosition.Value;

            CurrentPosition = actualPos.Facing switch
            {
                PacDirection.NORTH => GetValidPositionInGrid(actualPos.X, actualPos.Y + 1, actualPos.Facing),
                PacDirection.SOUTH => GetValidPositionInGrid(actualPos.X, actualPos.Y - 1, actualPos.Facing),
                PacDirection.EAST => GetValidPositionInGrid(actualPos.X + 1, actualPos.Y, actualPos.Facing),
                PacDirection.WEST => GetValidPositionInGrid(actualPos.X - 1, actualPos.Y, actualPos.Facing),
                _ => throw new InvalidOperationException()
            };
        }

        public PacPosition? Report() => CurrentPosition;

        #endregion
    }

}
