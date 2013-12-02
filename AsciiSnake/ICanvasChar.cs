using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace dk.ChrisGulddahl.AsciiSnake
{
	public interface ICanvasChar
	{
		char Character { get; set; }
		Point Position { get; set; }
		ConsoleColor Color { get; set; }
	}
}
