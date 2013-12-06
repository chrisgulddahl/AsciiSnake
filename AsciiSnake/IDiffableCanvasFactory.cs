using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dk.ChrisGulddahl.AsciiSnake
{
	public interface IDiffableCanvasFactory
	{
		/// <summary>
		/// Create new ICanvas instance.
		/// </summary>
		/// <returns></returns>
		IDiffableCanvas Create();
	}
}
