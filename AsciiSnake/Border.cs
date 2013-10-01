using System;
using System.Drawing;

namespace dk.ChrisGulddahl.AsciiSnake
{
	class Border : IBorder
	{
		private int _consoleHeightAtLastDraw = 0;
		private int _consoleWidthAtLastDraw = 0;
		private readonly char _borderTop, _borderRight, _borderBottom, _borderLeft;

		public Border(IConsoleWrapper console, char borderTop, char borderRight, char borderBottom, char borderLeft)
		{
			Console = console;
			_borderTop = borderTop;
			_borderRight = borderRight;
			_borderBottom = borderBottom;
			_borderLeft = borderLeft;
		}

		public Border(IConsoleWrapper console, char borderTopAndBottom, char borderLeftAndRight)
			: this(console, borderTopAndBottom, borderLeftAndRight, borderTopAndBottom, borderLeftAndRight) { }

		public IConsoleWrapper Console { get; private set;  }

		public void Draw()
		{
			_consoleHeightAtLastDraw = Console.WindowHeight;
			_consoleWidthAtLastDraw = Console.WindowWidth;

			DrawWithChars(_borderTop, _borderRight, _borderBottom, _borderLeft);
		}

		private void Undraw()
		{
			DrawWithChars(Config.NullChar, Config.NullChar, Config.NullChar, Config.NullChar);
		}

		public void Redraw()
		{
			if (NeedsRedraw())
			{
				Undraw();
				Draw();
			}
		}

		public bool NeedsRedraw()
		{
			return Console.WindowHeight != _consoleHeightAtLastDraw || Console.WindowWidth != _consoleWidthAtLastDraw;
		}

		public bool ContainsPosition(Point position)
		{
			return position.X <= 0 || position.X >= _consoleWidthAtLastDraw - 1
			       || position.Y <= 0 || position.Y >= _consoleHeightAtLastDraw - 2;
		}

		private void DrawWithChars(char topChar, char rightChar, char bottomChar, char leftChar)
		{
			Console.SetCursorPosition(0, 0);
			Console.Write(new string(topChar, _consoleWidthAtLastDraw));
			Console.SetCursorPosition(0, _consoleHeightAtLastDraw - 2);
			Console.Write(new string(bottomChar, _consoleWidthAtLastDraw));

			for (int row = 1; row < _consoleHeightAtLastDraw - 2; row++)
			{
				Console.SetCursorPosition(0, row);
				Console.Write(leftChar);
				Console.SetCursorPosition(_consoleWidthAtLastDraw - 1, row);
				Console.Write(rightChar);
			}
		}
	}
}
