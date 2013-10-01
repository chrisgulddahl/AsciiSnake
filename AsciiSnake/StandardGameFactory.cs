using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dk.ChrisGulddahl.AsciiSnake
{
	class StandardGameFactory : IGameFactory
	{
		public IGame CreateGame()
		{
			return new Game(new StandardGameFactory());
		}

		public IConsoleWrapper CreateConsole()
		{
			return new ConsoleWrapper();
		}

		public IBorder CreateBorder(IGame game)
		{
			return new BorderWithScore(
				new TitledBorder(
					new Border(game.Console, Config.BorderTopChar, Config.BorderLeftChar), "ASCII Snake. Source code at chrisgulddahl.dk", "[W] [A] [S] [D] Control. [Q] Quit. [M] Mute"),
				game);
		}

		public ISnake CreateSnake(IConsoleWrapper console)
		{
			return new Snake(console, console.WindowWidth / 2, console.WindowHeight / 2, Config.SnakeHeadDrawingChar,
							 Config.SnakeBodyDrawingChar, ConsoleColor.DarkGreen);
		}

		public IApples CreateApples(ISnake snake)
		{
			return new Apples(5, 40, Config.AppleDrawingChar, ConsoleColor.Red, snake);
		}

		public ISoundManager CreateSoundManager()
		{
			return new SoundManager();
		}
	}
}
