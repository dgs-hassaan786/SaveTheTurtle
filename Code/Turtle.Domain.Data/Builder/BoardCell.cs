namespace Turtle.Domain.Data.Builder
{
    /// <summary>
    /// Demonstrating the cell of board
    /// </summary>
    public class BoardCell
    {
        /// <summary>
        /// Representing the Cell number
        /// </summary>
        public int YVal { get; set; }
        /// <summary>
        /// Cell has mine or not
        /// </summary>
        public bool HasMine { get; set; } = false;
        /// <summary>
        /// Cell has exit point or not
        /// </summary>
        public bool IsExitPoint { get; set; } = false;

        public BoardCell(int yCoordinate, bool hasMine, bool isExitPoint)
        {
            YVal = yCoordinate;
            HasMine = hasMine;
            IsExitPoint = isExitPoint;
        }
    }
}
