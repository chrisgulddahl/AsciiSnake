namespace dk.ChrisGulddahl.AsciiSnake
{
	interface IGameFactory
	{
		IConfig Config { get; }

		IConsoleWrapper Console { get; }

		IDiffFlushableCanvas DiffFlushableCanvas { get; }

		IGame CreateGame();

		IBorder CreateBorder(IGame game);

		ISnake CreateSnake(IConsoleWrapper console);

		IApples CreateApples(ISnake snake);

		ISoundManager CreateSoundManager();
	}
}
