using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dk.ChrisGulddahl.AsciiSnake
{
	class DiffableDiffableCanvasFactory : IDiffableCanvasFactory
	{
		private IConsoleWrapper _console;
		private IConfig _config;

		/// <summary>
		/// Instantiate factory for DiffableCanvas.
		/// </summary>
		/// <param name="console">Console wrapper for canvas to write to.</param>
		/// <param name="config">Config for determining text colors.</param>
		public DiffableDiffableCanvasFactory(IConsoleWrapper console, IConfig config)
		{
			_console = console;
			_config = config;
		}

		public IDiffableCanvas Create()
		{
			return new DiffableCanvas(_console, _config);
		}
	}
}
