using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Collections.Generic;
using System.Drawing;

namespace dk.ChrisGulddahl.AsciiSnake
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			IGameFactory gameFactory = new StandardGameFactory();
			var game = gameFactory.CreateGame();
			game.Start();
		}
	}
}
