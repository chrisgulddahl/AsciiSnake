

using System;

namespace dk.ChrisGulddahl.AsciiSnake
{
	class Config
	{
		public const ConsoleColor DefaultConsoleForeground = ConsoleColor.Black;
		public const char NullChar = (char) 0x00;
		public const char BorderTopChar = '-';
		public const char BorderRightChar = '|';
		public const char BorderBottomChar = '-';
		public const char BorderLeftChar = '|';
		public static char SnakeHeadDrawingChar = 'O';
		public static char SnakeBodyDrawingChar = 'o';
		public static char AppleDrawingChar = '@';

	}
}
