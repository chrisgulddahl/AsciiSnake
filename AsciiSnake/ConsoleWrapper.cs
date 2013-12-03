using System;

namespace dk.ChrisGulddahl.AsciiSnake
{
	public class ConsoleWrapper : IConsoleWrapper
	{
		private int _windowHeight;
		private int _windowWidth;

		public ConsoleWrapper()
		{
			RefreshWindowDimensions();
		}

		public int WindowHeight
		{
			get { return _windowHeight; }
		}

		public int WindowWidth
		{
			get { return _windowWidth; }
		}

		public System.Text.Encoding OutputEncoding
		{
			get { return Console.OutputEncoding; }
			set { Console.OutputEncoding = value; }
		}

		public void SetCursorPosition(int left, int top)
		{
			Console.SetCursorPosition(left, top);
		}

		public void Write(char character)
		{
			Console.Write(character);
		}

		public void Write(string str)
		{
			Console.Write(str);
		}

		public bool CursorVisible
		{
			get { return Console.CursorVisible; }
			set { Console.CursorVisible = value; }
		}


		public ConsoleColor BackgroundColor
		{
			get { return Console.BackgroundColor; }
			set { Console.BackgroundColor = value; }
		}

		public ConsoleColor ForegroundColor
		{
			get { return Console.ForegroundColor; }
			set { Console.ForegroundColor = value; }
		}

		public string Title
		{
			get { return Console.Title; }
			set { Console.Title = value; }
		}

		public void Clear()
		{
			Console.Clear();
		}

		public bool KeyAvailable
		{
			get { return Console.KeyAvailable; }
		}

		public ConsoleKeyInfo ReadKey(bool intercept)
		{
			return Console.ReadKey(intercept);
		}

		public ConsoleKeyInfo ReadKey()
		{
			return Console.ReadKey();
		}


		public void RefreshWindowDimensions()
		{
			_windowHeight = Console.WindowHeight;
			_windowWidth = Console.WindowWidth;
		}
	}
}
