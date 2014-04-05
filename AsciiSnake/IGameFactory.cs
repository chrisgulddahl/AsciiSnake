namespace dk.ChrisGulddahl.AsciiSnake
{
	/// <summary>
	/// Factory for IGame. Creates IGames and their dependencies.
	/// </summary>
	interface IGameFactory
	{
		IConfig Config { get; }

		IConsoleWrapper Console { get; }

		IDiffFlushableCanvas DiffFlushableCanvas { get; }

		IGame GetGame();

		IBorder GetBorder(IGame game);

		ISnake GetSnake(IConsoleWrapper console);

		IApples GetApples(ISnake snake);

		ISoundManager GetSoundManager();
	}
}
