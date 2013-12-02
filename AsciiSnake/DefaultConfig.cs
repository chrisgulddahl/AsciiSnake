using System;

namespace dk.ChrisGulddahl.AsciiSnake
{
	public class DefaultConfig : IConfig
	{
		public DefaultConfig()
		{
			ConsoleForeground = ConsoleColor.Black;
			ConsoleBackground = ConsoleColor.White;
			NullChar = (char)0x00;
			BorderTopChar = '-';
			BorderRightChar = '|';
			BorderBottomChar = '-';
			BorderLeftChar = '|';
			SnakeHeadDrawingChar = 'O';
			SnakeBodyDrawingChar = 'o';
			AppleDrawingChar = '@';
			TickTime = 80;
			ServerPort = 26676;
			MinAppleCount = 5;
			AppleLifetime = 100;
			AppleColor = ConsoleColor.Red;
			SnakeColor = ConsoleColor.DarkGreen;
		}

		public ConsoleColor ConsoleForeground { get; set; }
		public ConsoleColor ConsoleBackground { get; set; }
		public char NullChar { get; set; }
		public char BorderTopChar { get; set; }
		public char BorderRightChar { get; set; }
		public char BorderBottomChar { get; set; }
		public char BorderLeftChar { get; set; }
		public char SnakeHeadDrawingChar { get; set; }
		public char SnakeBodyDrawingChar { get; set; }
		public char AppleDrawingChar { get; set; }
		public int TickTime { get; set; }
		public int ServerPort { get; set; }
		public int MinAppleCount { get; set; }
		public int AppleLifetime { get; set; }
		public ConsoleColor AppleColor { get; set; }
		public ConsoleColor SnakeColor { get; set; }
	}
}
