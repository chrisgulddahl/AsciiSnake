using System;

namespace dk.ChrisGulddahl.AsciiSnake
{
	public interface IConfig
	{
		ConsoleColor ConsoleForeground { get; set; }
		ConsoleColor ConsoleBackground { get; set; }
		char NullChar { get; set; }
		char BorderTopChar { get; set; }
		char BorderRightChar { get; set; }
		char BorderBottomChar { get; set; }
		char BorderLeftChar { get; set; }
		char SnakeHeadDrawingChar { get; set; }
		char SnakeBodyDrawingChar { get; set; }
		char AppleDrawingChar { get; set; }
		int TickTime { get; set; }
		int ServerPort { get; set; }
		int MinAppleCount { get; set; }
		int AppleLifetime { get; set; }
		ConsoleColor AppleColor { get; set; }
		ConsoleColor SnakeColor { get; set; }
	}
}