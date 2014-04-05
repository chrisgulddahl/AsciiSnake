using System.Drawing;

namespace dk.ChrisGulddahl.AsciiSnake
{
	public class Border : IBorder
	{
		private int _height;
		private int _width;
		private IConfig Config { get; set; }

		public Border(IConfig config, int width, int height)
		{
			Config = config;
			_width = width;
			_height = height;
		}

		public void Draw(ICanvas canvas)
		{
			_width = canvas.Width;
			_height = canvas.Height;

			canvas.DrawString(new string(Config.BorderTopChar, _width), new Point(0, 0), Direction.East, Config.ConsoleForeground);
			canvas.DrawString(new string(Config.BorderBottomChar, _width), new Point(0, _height - 2), Direction.East, Config.ConsoleForeground);
			canvas.DrawString(new string(Config.BorderLeftChar, _height - 2), new Point(0, 1), Direction.South, Config.ConsoleForeground);
			canvas.DrawString(new string(Config.BorderRightChar, _height - 2), new Point(_width - 1, 1), Direction.South, Config.ConsoleForeground);
		}

		public bool ContainsPosition(Point position)
		{
			return position.X <= 0 || position.X >= _width - 1
				   || position.Y <= 0 || position.Y >= _height - 2;
		}

		public void Reset()
		{
			return; // Do nothing
		}
	}
}
