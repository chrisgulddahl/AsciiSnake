using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;

namespace dk.ChrisGulddahl.AsciiSnake
{
	public class Canvas : ICanvas
	{
		private Dictionary<Point, LinkedList<ICanvasChar>> _chars = new Dictionary<Point, LinkedList<ICanvasChar>>();

		public Canvas(IConsoleWrapper console, IConfig config)
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
			var newCanvasChar = new CanvasChar(pos, c, color);
			if (_chars.ContainsKey(pos))
			{
				var bucket = _chars[pos];
				bucket.AddFirst(newCanvasChar);
			}
			else
			{
				var bucket = new LinkedList<ICanvasChar>();
				bucket.AddFirst(newCanvasChar);
				_chars.Add(pos, bucket);
			}
		}

		public ICanvasChar TopCharAtPos(Point pos)
		{
			return _chars.ContainsKey(pos) ? _chars[pos].First.Value : null;
		}

		public void Reset()
		{
			_chars = new Dictionary<Point, LinkedList<ICanvasChar>>();
		}

		public ICanvas Diff(ICanvas canvasInput)
		{
			ICanvas diff = new Canvas(Console, Config);
			var currentTopChars = this.Select(l => l.First()).ToArray();
			var inputTopChars = canvasInput.Select(l => l.First()).ToArray();
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


		public IEnumerator<IEnumerable<ICanvasChar>> GetEnumerator()
		{
			return _chars.Values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _chars.Values.GetEnumerator();
		}


		public void WriteToConsole()
		{
			foreach (var firstChar in _chars.Select(pointCanvasChar => pointCanvasChar.Value.First.Value))
			{
				Console.ForegroundColor = firstChar.Color;
				Console.SetCursorPosition(firstChar.Position.X, firstChar.Position.Y);
				Console.Write(firstChar.Character);
				//System.Console.WriteLine(firstChar.Character + " at " + firstChar.Position);
			}
		}

		public void SetData(Dictionary<Point, LinkedList<ICanvasChar>> chars)
		{
			_chars = chars;
		}

		public object Clone()
		{
			var clone = new Canvas(Console, Config);
			clone.SetData(_chars);
			return clone;
		}
	}
}
