using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dk.ChrisGulddahl.AsciiSnake
{
	class BorderWithScore : IBorder
	{
		private IBorder _border;
		private IGame _game;
		private int _score;

		public BorderWithScore(IBorder border, IGame game)
		{
			_border = border;
			_game = game;
			Console = _border.Console;
			_score = 0;
		}

		public IConsoleWrapper Console { get; private set; }

		private void DrawScore()
		{
			_score = _game.Score;
			Console.SetCursorPosition(3, Console.WindowHeight - 2);
			Console.Write("Score: " + _score);
		}

		public void Draw()
		{
			_border.Draw();
			DrawScore();
		}

		public bool ContainsPosition(System.Drawing.Point position)
		{
			return _border.ContainsPosition(position);
		}

		public void Redraw()
		{
			_border.Redraw();
			DrawScore();
		}

		public bool NeedsRedraw()
		{
			return _border.NeedsRedraw() || _game.Score != _score;
		}
	}
}
