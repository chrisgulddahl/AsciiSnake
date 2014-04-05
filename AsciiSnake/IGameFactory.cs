namespace dk.ChrisGulddahl.AsciiSnake
{
	/// <summary>
	/// Factory for IGame. Create and configures an IGame.
	/// </summary>
	interface IGameFactory
	{
		/// <summary>
		/// Create and configure a Snake game.
		/// </summary>
		/// <returns></returns>
		IGame CreateGame();
	}
}
