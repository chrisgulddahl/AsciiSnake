using System;

namespace dk.ChrisGulddahl.AsciiSnake
{
	interface IConsoleWrapper
	{
		int WindowHeight { get; }

		int WindowWidth { get; }

		bool CursorVisible { get; set; }

		ConsoleColor BackgroundColor { get; set; }

		ConsoleColor ForegroundColor { get; set; }

		string Title { get; set; }

		bool KeyAvailable { get; }

		void SetCursorPosition(int left, int top);

		void Write(char character);

		void Write(string str);

		void Clear();

		ConsoleKeyInfo ReadKey();

		ConsoleKeyInfo ReadKey(bool intercept);

		void RefreshWindowDimensions();

	}
}
