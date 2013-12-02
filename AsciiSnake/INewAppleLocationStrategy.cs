using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace dk.ChrisGulddahl.AsciiSnake
{
	public interface INewAppleLocationStrategy
	{
		Point GetNew();
	}
}
