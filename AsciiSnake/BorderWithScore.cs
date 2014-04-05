using System.Drawing;

namespace dk.ChrisGulddahl.AsciiSnake
{
	class BorderWithScore : IBorder
	{
		private IBorder _border;
		private IGameScore _score;

		private IConfig Config { get; set; }

		public BorderWithScore(IBorder border, IGameScore score, IConfig config)
		{
			_border = border;
			_score = score;
			Config = config;
		}

		private void DrawScore(ICanvas canvas)
		{
			canvas.DrawString("Score: " + _score.Value, new Point(3, canvas.Height - 2), Direction.East, Config.ConsoleForeground);
		}

		public void Draw(ICanvas canvas)
		{
			_border.Draw(canvas);
			DrawScore(canvas);
		}

		public bool ContainsPosition(Point position)
		{
			return _border.ContainsPosition(position);
		}

		public void Reset()
		{
			_border.Reset();
		}
	}
}
