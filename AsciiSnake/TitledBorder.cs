using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dk.ChrisGulddahl.AsciiSnake
{
	class TitledBorder : IBorder
	{
		private IBorder _border;
		private string _title;
		private string _subtitle;

		public TitledBorder(IBorder border, string title, string subtitle)
		{
			_border = border;
			_title = title;
			_subtitle = subtitle;
			Console = _border.Console;
		}

		public IConsoleWrapper Console { get; private set; }

		private void DrawTitles()
		{
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

		public bool ContainsPosition(System.Drawing.Point position)
		{
			return _border.ContainsPosition(position);
		}

		public void Redraw()
		{
			_border.Redraw();
			DrawTitles();
		}

		public bool NeedsRedraw()
		{
			return _border.NeedsRedraw();
		}
	}
}
