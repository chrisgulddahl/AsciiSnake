using System;

namespace dk.ChrisGulddahl.AsciiSnake
{
	class DefaultGameFactory : IGameFactory
	{
		public DefaultGameFactory()
		{
			/*Initialize single instances*/
			Config = new DefaultConfig();
			Console = new ConsoleWrapper();
			DiffFlushableCanvas = new DiffFlushableCanvas(Config, new DiffableDiffableCanvasFactory(Console, Config));
		}

		public IConfig Config { get; private set; }

		public IConsoleWrapper Console { get; private set; }

		public IDiffFlushableCanvas DiffFlushableCanvas { get; private set; }


		public IGame CreateGame()
		{
			return new Game(this);
		}

		public IConsoleWrapper CreateConsole()
		{
			return new ConsoleWrapper();
		}

		public IBorder CreateBorder(IGame game)
		{
			return new BorderWithScore(
				new TitledBorder(
					new Border(DiffFlushableCanvas, Config), DiffFlushableCanvas, Config, "ASCII Snake. Source code at chrisgulddahl.dk", "[W] [A] [S] [D] Control. [Q] Quit. [M] Mute")
					, game, DiffFlushableCanvas, Config);
		}

		public ISnake CreateSnake(IConsoleWrapper console)
		{
			return new Snake(DiffFlushableCanvas, Config, console.WindowWidth / 2, console.WindowHeight / 2);
		}

		public IApples CreateApples(ISnake snake)
		{
			return new Apples(DiffFlushableCanvas, Config, snake, new RandomNewAppleLocationStrategy(Console));
		}

		public ISoundManager CreateSoundManager()
		{
			return new SoundManager();
		}
	}
}
