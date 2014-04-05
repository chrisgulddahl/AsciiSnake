using System.Drawing;

namespace dk.ChrisGulddahl.AsciiSnake
{
	public class TitledBorder : IBorder
	{
		private IBorder _border;
		private string _title;
		private string _subtitle;

		private IConfig Config { get; set; }

		public TitledBorder(IBorder border, IConfig config, string title, string subtitle)
		{
			_border = border;
			_title = title;
			_subtitle = subtitle;
			Config = config;
		}

		public void Draw(ICanvas canvas)
		{
			_border.Draw(canvas);
			canvas.DrawString(_title, new Point(3, 0), Direction.East, Config.ConsoleForeground);
			canvas.DrawString(_subtitle, new Point(canvas.Width - _subtitle.Length - 3, canvas.Height - 2), Direction.East, Config.ConsoleForeground);
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
