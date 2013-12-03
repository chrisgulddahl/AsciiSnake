using System.Drawing;

namespace dk.ChrisGulddahl.AsciiSnake
{
	public enum Direction
	{
		North = 1,
		East = 2,
		South = -1,
		West = -2,
		None = 0
	}

	public class DirectionUtil
	{
		public static bool AreOpposite(Direction dir1, Direction dir2)
		{
			return ((int)dir1 + (int)dir2) == 0;
		}

		public static Point OffsetPosition(Point pos, Direction dir)
		{
			switch (dir)
			{
				case Direction.North: return new Point(pos.X, pos.Y-1);
				case Direction.South: return new Point(pos.X, pos.Y + 1);
				case Direction.East: return new Point(pos.X+1, pos.Y);
				case Direction.West: return new Point(pos.X - 1, pos.Y);
				default: return pos;
			}
		}
	}
}