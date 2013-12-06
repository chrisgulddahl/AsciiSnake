using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace dk.ChrisGulddahl.AsciiSnake
{
	public interface IDiffFlushableCanvas : ICanvas
	{
		/// <summary>
		/// Write changes (since last call of FlushChangesToConsole) to console.
		/// </summary>
		void FlushChanges();

		/// <summary>
		/// Write entire canvas to console including any pending changes.
		/// </summary>
		void WriteCurrent();
	}
}
