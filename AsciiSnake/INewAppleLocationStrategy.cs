﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace dk.ChrisGulddahl.AsciiSnake
{
	public interface INewAppleLocationStrategy
	{
		/// <summary>
		/// Get position for a new apple.
		/// </summary>
		/// <returns>Position for a new apple.</returns>
		Point GetNew();
	}
}
