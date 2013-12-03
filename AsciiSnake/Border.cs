using System.Drawing;

namespace dk.ChrisGulddahl.AsciiSnake
{
	public class Border : IBorder
	{
		private int _consoleHeightAtLastDraw = 0;
		private int _consoleWidthAtLastDraw = 0;

		public Border(IConsoleWrapper console, ICanvas canvas, IConfig config)
		{
			Console = console;
			Config = config;
			Canvas = canvas;
		}

		private IConsoleWrapper Console { get; set; }

		private ICanvas Canvas { get; set; }

		private IConfig Config { get; set; }

		public void Draw()
		{
			_consoleHeightAtLastDraw = Console.WindowHeight;
			_consoleWidthAtLastDraw = Console.WindowWidth;

			DrawWithChars(Config.BorderTopChar, Config.BorderRightChar, Config.BorderBottomChar, Config.BorderLeftChar);
		}

		public bool ContainsPosition(Point position)
		{
			return position.X <= 0 || position.X >= _consoleWidthAtLastDraw - 1
				   || position.Y <= 0 || position.Y >= _consoleHeightAtLastDraw - 2;
		}

		private void DrawWithChars(char topChar, char rightChar, char bottomChar, char leftChar)
		{
			for (int col = 0; col < _consoleWidthAtLastDraw - 2; col++)
			{
				Canvas.DrawChar(new Point(col, 0), topChar);
				Canvas.DrawChar(new Point(col, _consoleHeightAtLastDraw - 2), bottomChar);
			}
			for (int row = 1; row < _consoleHeightAtLastDraw - 2; row++)
			{
				Canvas.DrawChar(new Point(0, row), leftChar);
				Canvas.DrawChar(new Point(_consoleWidthAtLastDraw - 1, row), rightChar);
			}
		}
	}
}
