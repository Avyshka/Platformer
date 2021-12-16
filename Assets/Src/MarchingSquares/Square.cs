namespace Platformer.MarchingSquares
{
    public class Square
    {
        public ControlNode TopLeft;
        public ControlNode TopRight;
        public ControlNode BottomLeft;
        public ControlNode BottomRight;

        public Square(
            ControlNode topLeft, 
            ControlNode topRight,
            ControlNode bottomLeft,
            ControlNode bottomRight)
        {
            TopLeft = topLeft;
            TopRight = topRight;
            BottomLeft = bottomLeft;
            BottomRight = bottomRight;
        }
    }
}