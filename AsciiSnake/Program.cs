using System;
using System.Linq;
using System.Text;
using System.Threading;

namespace dk.ChrisGulddahl.AsciiSnake
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			IGameFactory gameFactory = new DefaultGameFactory();
			var game = gameFactory.CreateGame();
			game.Start();
		}
	}
}
