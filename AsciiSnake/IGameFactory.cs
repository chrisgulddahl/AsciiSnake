using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dk.ChrisGulddahl.AsciiSnake
{
	interface IGameFactory
	{
		IGame CreateGame();

		IConsoleWrapper CreateConsole();

		IBorder CreateBorder(IGame game);

		ISnake CreateSnake(IConsoleWrapper console);

		IApples CreateApples(ISnake snake);

		ISoundManager CreateSoundManager();
	}
}
