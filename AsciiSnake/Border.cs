using System.Drawing;

namespace dk.ChrisGulddahl.AsciiSnake
{
	public class Border : IBorder
	{
		public Border(ICanvas canvas, IConfig config)
		{
			Config = config;
			Canvas = canvas;
		}

		private ICanvas Canvas { get; set; }

		private IConfig Config { get; set; }

		public void Draw()
		{
			var height = Canvas.Height;
			var width = Canvas.Width;

			Canvas.DrawString(new string(Config.BorderTopChar, width), new Point(0, 0), Direction.East, Config.ConsoleForeground);
			Canvas.DrawString(new string(Config.BorderBottomChar, width), new Point(0, height - 2), Direction.East, Config.ConsoleForeground);
			Canvas.DrawString(new string(Config.BorderLeftChar, height - 2), new Point(0, 1), Direction.South, Config.ConsoleForeground);
			Canvas.DrawString(new string(Config.BorderRightChar, height - 2), new Point(width - 1, 1), Direction.South, Config.ConsoleForeground);
		}

		public bool ContainsPosition(Point position)
		{
			return position.X <= 0 || position.X >= Canvas.Width - 1
				   || position.Y <= 0 || position.Y >= Canvas.Height - 2;
		}
	}
}
