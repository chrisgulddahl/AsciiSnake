using System;

namespace dk.ChrisGulddahl.AsciiSnake
{
	class DefaultGameFactory : IGameFactory
	{
		public IGame CreateGame()
		{
			var config = new DefaultConfig();
			var console = new ConsoleWrapper();
			var canvas = new DiffFlushableCanvas(config, new DiffableDiffableCanvasFactory(console, config));
			var soundManager = new SoundManager();
			var snake = new Snake(config, console.WindowWidth / 2, console.WindowHeight / 2);
			var apples = new Apples(config, snake, new RandomNewAppleLocationStrategy(console));
			var gameScore = new SnakeLengthGameScore(snake);
			var border = new BorderWithScore(
				new TitledBorder(
					new Border(config, canvas.Width, canvas.Height), config, "ASCII Snake. Source code at chrisgulddahl.dk", "[W] [A] [S] [D] Control. [Q] Quit. [M] Mute")
					, gameScore, config);
			return new Game(config, console, canvas, gameScore, soundManager, border, snake, apples);
		}
	}
}
