namespace Turtle.Domain.Data.Builder
{
    public class BoardCell
    {
        public int YVal { get; set; }
        public bool HasMine { get; set; } = false;
        public bool IsExitPoint { get; set; } = false;

        public BoardCell(int yCoordinate, bool hasMine, bool isExitPoint)
        {
            YVal = yCoordinate;
            HasMine = hasMine;
            IsExitPoint = isExitPoint;
        }
    }
}
