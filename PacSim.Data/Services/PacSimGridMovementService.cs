using System;
using System.Diagnostics;
using PacSim.Models;

namespace PacSim.Services
{
    /// <summary>
    /// Represents an implementation of IPacMovementService in a two dimensional grid shape.
    /// The size of the grid is readonly and determined at instantiation of the service.
    /// 
    /// The 'Pac' can be placed and moved within the grid. It is not allowed to be placed or moved outside the grid size.
    /// Only one Pac may exist per instance of the service.
    /// 
    /// Inputs are validated based on their type. Exceptions are thrown for any invalid or unrecognized inputs.
    /// </summary>
    public class PacSimGridMovementService : IPacSimMovementService
    {
        const string NOT_PLACED_MESSAGE = "Pac has no current position. You must call PLACE first.";

        public int GridWidth { get; }
        public int GridHeight { get; }

        PacPosition? currentPacPosition;
        public PacPosition? CurrentPacPosition
        {
            get => currentPacPosition;
            set
            {
                currentPacPosition = value;
                Debug.WriteLine($"New Pac Position {currentPacPosition}");
            }
        }

        public PacSimGridMovementService(int gridWidth, int gridHeight)
        {
            if (gridWidth <= 0 || gridHeight <= 0) throw new ArgumentException("Invalid grid dimensions. Width and Height must be > 0.");

            GridWidth = gridWidth;
            GridHeight = gridHeight;

            Debug.WriteLine($"Pac grid of {GridWidth}x{GridHeight} created.");
        }

        /// <summary>
        /// Generates a new immutable PacPosition based on inputs.
        /// Inputs are validated against the current grid dimensions.
        ///
        /// Throws ArgumentOutOfRangeException for any values outside the dimensions of the grid.
        /// </summary>
        PacPosition GetValidPositionInGrid(int newX, int newY, PacDirection newFacing) => new PacPosition(newX, newY, newFacing) switch
        {
            var (x, _, _) when x < 0 || x >= GridWidth => throw new ArgumentOutOfRangeException($"Invalid X. Valid values are 0-{GridWidth}"),
            var (_, y, _) when y < 0 || y >= GridHeight => throw new ArgumentOutOfRangeException($"Invalid Y. Valid values are 0-{GridHeight}"),
            var newPosition => newPosition
        };

        #region IPacSimMovementService

        /// <summary>
        /// Places a 'Pac' on the grid.
        /// Any position outside the grid will throw an exception.
        /// Axis indexs are 0-N with 0,0 being the SOUTH WEST most corner.
        /// Only one Pac exists. Multiple calls to Place() will reposition any existing Pac.
        /// </summary>
        /// <param name="x">The index of the Pac on the grid's x (width) axis.</param>
        /// <param name="y">The index of the Pac on the grid's y (height) axis.</param>
        /// <param name="facing">The direction the Pac will face.</param>
        /// <returns></returns>
        public PacPosition Place(int x, int y, PacDirection facing) =>
            (CurrentPacPosition = GetValidPositionInGrid(x, y, facing)) ?? throw new InvalidOperationException();

        /// <summary>
        /// Faces the Pac 90 degrees to the Left
        /// Example: If Pac was facing NORTH, it will face WEST after calling Left()
        ///
        /// Throws InvalidOperationException if the Pac has not been placed.
        /// </summary>
        public void Left()
        {
            if (CurrentPacPosition is null) throw new InvalidOperationException(NOT_PLACED_MESSAGE);

            var actualPos = CurrentPacPosition.Value;

            CurrentPacPosition = GetValidPositionInGrid(actualPos.X, actualPos.Y, actualPos.Facing.ToTheLeft());
        }

        /// <summary>
        /// Faces the Pac 90 degrees to the Right
        /// Example: If Pac was facing NORTH, it will face EAST after calling Right()
        ///
        /// Throws InvalidOperationException if the Pac has not been placed.
        /// </summary>
        public void Right()
        {
            if (CurrentPacPosition is null) throw new InvalidOperationException(NOT_PLACED_MESSAGE);

            var actualPos = CurrentPacPosition.Value;

            CurrentPacPosition = GetValidPositionInGrid(actualPos.X, actualPos.Y, actualPos.Facing.ToTheRight());
        }

        /// <summary>
        /// Moves the Pac one unit forward in the direction it is currently facing.
        /// Example: REPORT -> '0,0,NORTH' -> Move() -> REPORT -> '0,1,NORTH'
        ///
        /// Throws ArgumentOutOfRangeException if new position would fall outside the grid.
        /// Throws InvalidOperationException if the Pac has not been placed.
        /// </summary>
        public void Move()
        {
            if (CurrentPacPosition is null) throw new InvalidOperationException(NOT_PLACED_MESSAGE);

            var actualPos = CurrentPacPosition.Value;

            CurrentPacPosition = actualPos.Facing switch
            {
                PacDirection.NORTH => GetValidPositionInGrid(actualPos.X, actualPos.Y + 1, actualPos.Facing),
                PacDirection.SOUTH => GetValidPositionInGrid(actualPos.X, actualPos.Y - 1, actualPos.Facing),
                PacDirection.EAST => GetValidPositionInGrid(actualPos.X + 1, actualPos.Y, actualPos.Facing),
                PacDirection.WEST => GetValidPositionInGrid(actualPos.X - 1, actualPos.Y, actualPos.Facing),
                _ => throw new InvalidOperationException()
            };
        }

        /// <summary>
        /// Returns the current 'Pac' position in the grid.
        /// Null if not yet placed.
        /// </summary>
        /// <returns>The current position of the Pac in the grid.</returns>
        public PacPosition? Report() => CurrentPacPosition;

        #endregion
    }

}
