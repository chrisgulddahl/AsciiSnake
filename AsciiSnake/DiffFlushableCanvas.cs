using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace dk.ChrisGulddahl.AsciiSnake
{
	public class DiffFlushableCanvas : IDiffFlushableCanvas
	{

		private IDiffableCanvas _flushedCanvas; //saves state of latest flushed canvas
		private IDiffableCanvas _newCanvas; //new chars are written here - dirty until FlushChangesToConsole or WriteCurrentToConsole is called

		public DiffFlushableCanvas(IConsoleWrapper console, IConfig config)
		{
			Console = console;
			Config = config;
			_flushedCanvas = new DiffableCanvas(Console, Config);
			_newCanvas = new DiffableCanvas(Console, Config);
		}

		private IConsoleWrapper Console { get; set; }
		private IConfig Config { get; set; }

		public void FlushChangesToConsole()
		{
			_flushedCanvas.Diff(_newCanvas).WriteToConsole();
			_flushedCanvas = _newCanvas;
			_newCanvas = new DiffableCanvas(Console, Config);
		}

		public void WriteCurrentToConsole()
		{
			_newCanvas.WriteToConsole();
			_flushedCanvas = _newCanvas;
			_newCanvas = new DiffableCanvas(Console, Config);
		}

		public int Height { get { return _newCanvas.Height; } }

		public int Width { get { return _newCanvas.Width; } }

		public void DrawChar(Point pos, char c)
		{
			_newCanvas.DrawChar(pos, c);
		}

		public void DrawChar(Point pos, char c, ConsoleColor color)
		{
			_newCanvas.DrawChar(pos, c, color);
		}

		public void DrawString(string str, Point startPos, Direction direction, ConsoleColor color)
		{
			_newCanvas.DrawString(str, startPos, direction, color);
		}
	}
}
