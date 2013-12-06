using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace dk.ChrisGulddahl.AsciiSnake
{
	public class DiffFlushableCanvas : IDiffFlushableCanvas
	{
		private readonly IDiffableCanvasFactory _canvasFactory;
		private IDiffableCanvas _flushedCanvas; //saves state of latest flushed canvas
		private IDiffableCanvas _dirtyCanvas; //new chars are written here - dirty until FlushChangesToConsole or WriteCurrentToConsole is called

		public DiffFlushableCanvas(IConfig config, IDiffableCanvasFactory canvasFactory)
		{
			Config = config;
			_canvasFactory = canvasFactory;
			_flushedCanvas = _canvasFactory.Create();
			_dirtyCanvas = _canvasFactory.Create();
		}

		private IConfig Config { get; set; }

		public void FlushChanges()
		{
			_flushedCanvas.Diff(_dirtyCanvas).WriteToConsole();
			PersistDirty();
		}

		public void WriteCurrent()
		{
			_dirtyCanvas.WriteToConsole();
			PersistDirty();
		}

		public int Height { get { return _dirtyCanvas.Height; } }

		public int Width { get { return _dirtyCanvas.Width; } }

		public void DrawChar(Point pos, char c)
		{
			_dirtyCanvas.DrawChar(pos, c);
		}

		public void DrawChar(Point pos, char c, ConsoleColor color)
		{
			_dirtyCanvas.DrawChar(pos, c, color);
		}

		public void DrawString(string str, Point startPos, Direction direction, ConsoleColor color)
		{
			_dirtyCanvas.DrawString(str, startPos, direction, color);
		}

		private void PersistDirty()
		{
			_flushedCanvas = _dirtyCanvas;
			_dirtyCanvas = _canvasFactory.Create();
		}
	}
}
