using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace dk.ChrisGulddahl.AsciiSnake
{
	public class DiffFlushableCanvas : IDiffFlushableCanvas
	{

		private IDiffableCanvas _flushedDiffableCanvas; //saves state of latest flushed canvas
		private IDiffableCanvas _newDiffableCanvas; //new chars are written here - dirty until FlushChangesToConsole or WriteCurrentToConsole is called

		public DiffFlushableCanvas(IConsoleWrapper console, IConfig config)
		{
			Console = console;
			Config = config;
			_flushedDiffableCanvas = new DiffableCanvas(Console, Config);
			_newDiffableCanvas = new DiffableCanvas(Console, Config);
		}

		private IConsoleWrapper Console { get; set; }
		private IConfig Config { get; set; }

		public void FlushChangesToConsole()
		{
			_flushedDiffableCanvas.Diff(_newDiffableCanvas).WriteToConsole();
			_flushedDiffableCanvas = _newDiffableCanvas;
			_newDiffableCanvas = new DiffableCanvas(Console, Config);
		}

		public void WriteCurrentToConsole()
		{
			_newDiffableCanvas.WriteToConsole();
			_flushedDiffableCanvas = _newDiffableCanvas;
			_newDiffableCanvas = new DiffableCanvas(Console, Config);
		}

		public void DrawChar(Point pos, char c)
		{
			_newDiffableCanvas.DrawChar(pos, c);
		}

		public void DrawChar(Point pos, char c, ConsoleColor color)
		{
			_newDiffableCanvas.DrawChar(pos, c, color);
		}
	}
}
