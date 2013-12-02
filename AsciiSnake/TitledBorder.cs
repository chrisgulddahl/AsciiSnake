using System.Drawing;

namespace dk.ChrisGulddahl.AsciiSnake
{
	public class TitledBorder : IBorder
	{
		private IBorder _border;
		private string _title;
		private string _subtitle;

		public TitledBorder(IBorder border, IDiffFlushableCanvas canvas, IConsoleWrapper console, string title, string subtitle)
		{
			_border = border;
			_title = title;
			_subtitle = subtitle;
			Console = console;
			Canvas = canvas;
		}

		private IDiffFlushableCanvas Canvas { get; set; }

		private IConsoleWrapper Console { get; set; }

		private void DrawTitles()
		{
			//to DO: draw to canvas
			Console.SetCursorPosition(3, 0);
			Console.Write(_title);
			Console.SetCursorPosition(Console.WindowWidth - _subtitle.Length - 3, Console.WindowHeight - 2);
			Console.Write(_subtitle);
		}

		public void Draw()
		{
			_border.Draw();
			DrawTitles();
		}

		public bool ContainsPosition(Point position)
		{
			return _border.ContainsPosition(position);
		}
	}
}
