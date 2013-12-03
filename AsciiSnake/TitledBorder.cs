using System.Drawing;

namespace dk.ChrisGulddahl.AsciiSnake
{
	public class TitledBorder : IBorder
	{
		private IBorder _border;
		private string _title;
		private string _subtitle;

		public TitledBorder(IBorder border, IDiffFlushableCanvas canvas, IConfig config, string title, string subtitle)
		{
			_border = border;
			_title = title;
			_subtitle = subtitle;
			Config = config;
			Canvas = canvas;
		}

		private IDiffFlushableCanvas Canvas { get; set; }

		private IConfig Config { get; set; }

		public void Draw()
		{
			_border.Draw();
			Canvas.DrawString(_title, new Point(3, 0), Direction.East, Config.ConsoleForeground);
			Canvas.DrawString(_subtitle, new Point(Canvas.Width - _subtitle.Length - 3, Canvas.Height - 2), Direction.East, Config.ConsoleForeground);
		}

		public bool ContainsPosition(Point position)
		{
			return _border.ContainsPosition(position);
		}
	}
}
