using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;

namespace dk.ChrisGulddahl.AsciiSnake
{
	public class DiffableCanvas : IDiffableCanvas
	{
		private Dictionary<Point, ICanvasChar> _chars = new Dictionary<Point, ICanvasChar>();

		public DiffableCanvas(IConsoleWrapper console, IConfig config)
		{
			Console = console;
			Config = config;
		}

		private IConfig Config { get; set; }

		private IConsoleWrapper Console { get; set; }

		public void DrawChar(Point pos, char c)
		{
			DrawChar(pos, c, Config.ConsoleForeground);
		}

		public void DrawChar(Point pos, char c, ConsoleColor color)
		{
			_chars[pos] = new CanvasChar(pos, c, color);
		}

		public IDiffableCanvas Diff(IDiffableCanvas diffableCanvasInput)
		{
			IDiffableCanvas diff = new DiffableCanvas(Console, Config);
			var currentTopChars = this.ToArray();
			var inputTopChars = diffableCanvasInput.ToArray();
			var addedChars = inputTopChars.Except(currentTopChars);
			var removedChars = currentTopChars.Except(inputTopChars);
			foreach (var canvasChar in removedChars)
			{
				diff.DrawChar(canvasChar.Position, Config.NullChar, Config.ConsoleForeground);
			}
			foreach (var canvasChar in addedChars)
			{
				diff.DrawChar(canvasChar.Position, canvasChar.Character, canvasChar.Color);
			}
			return diff;
		}

		public IEnumerator<ICanvasChar> GetEnumerator()
		{
			return _chars.Values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _chars.Values.GetEnumerator();
		}

		public void WriteToConsole()
		{
			foreach (var firstChar in _chars.Select(pointCanvasChar => pointCanvasChar.Value))
			{
				Console.ForegroundColor = firstChar.Color;
				Console.SetCursorPosition(firstChar.Position.X, firstChar.Position.Y);
				Console.Write(firstChar.Character);
			}
		}
	}
}
