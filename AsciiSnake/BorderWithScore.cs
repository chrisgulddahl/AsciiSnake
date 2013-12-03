using System.Drawing;

namespace dk.ChrisGulddahl.AsciiSnake
{
	class BorderWithScore : IBorder
	{
		private IBorder _border;
		private IGame _game;

		public BorderWithScore(IBorder border, IGame game, ICanvas canvas, IConfig config)
		{
			_border = border;
			_game = game;
			Canvas = canvas;
			Config = config;
		}

		private ICanvas Canvas { get; set; }

		private IConfig Config { get; set; }

		private void DrawScore()
		{
			Canvas.DrawString("Score: " + _game.Score, new Point(3, Canvas.Height - 2), Direction.East, Config.ConsoleForeground);
		}

		public void Draw()
		{
			_border.Draw();
			DrawScore();
		}

		public bool ContainsPosition(Point position)
		{
			return _border.ContainsPosition(position);
		}
	}
}
