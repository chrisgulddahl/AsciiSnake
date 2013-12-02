using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace dk.ChrisGulddahl.AsciiSnake
{
	public class DiffFlushableCanvas : IDiffFlushableCanvas
	{

		private ICanvas _flushedCanvas; //saves state of latest flushed canvas
		private readonly ICanvas _newCanvas; //new chars are written here - dirty until FlushChangesToConsole or WriteCurrentToConsole is called

		public DiffFlushableCanvas(IConsoleWrapper console, IConfig config)
		{
			Console = console;
			Config = config;
			_flushedCanvas = new Canvas(Console, Config);
			_newCanvas = new Canvas(Console, Config);
		}

		private IConsoleWrapper Console { get; set; }
		private IConfig Config { get; set; }

		public void FlushChangesToConsole()
		{
			_flushedCanvas.Diff(_newCanvas).WriteToConsole();
			_flushedCanvas = (Canvas)_newCanvas.Clone();
			_newCanvas.Reset();
		}

		public void WriteCurrentToConsole()
		{
			_newCanvas.WriteToConsole();
			_flushedCanvas = (Canvas)_newCanvas.Clone();
			_newCanvas.Reset();
		}

		public void ClearChangesToCanvas()
		{
			_newCanvas.Reset();
		}

		public void DrawChar(Point pos, char c)
		{
			_newCanvas.DrawChar(pos, c);
		}

		public void DrawChar(Point pos, char c, ConsoleColor color)
		{
			_newCanvas.DrawChar(pos, c, color);
		}
	}
}
