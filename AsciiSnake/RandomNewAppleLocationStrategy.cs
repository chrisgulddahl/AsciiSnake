using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace dk.ChrisGulddahl.AsciiSnake
{
	public class RandomNewAppleLocationStrategy : INewAppleLocationStrategy
	{
		private readonly Random _rand = new Random();
		private IConsoleWrapper Console { get; set; }

		public RandomNewAppleLocationStrategy(IConsoleWrapper console)
		{
			Console = console;
		}

		public Point GetNew()
		{
			return new Point(_rand.Next(1, Console.WindowWidth - 2), _rand.Next(1, Console.WindowHeight - 3));
		}
	}
}
