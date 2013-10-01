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
