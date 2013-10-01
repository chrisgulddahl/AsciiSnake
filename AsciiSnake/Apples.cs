using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Drawing;

namespace dk.ChrisGulddahl.AsciiSnake
{
	class Apples : IApples
	{
		private Dictionary<Point, int> _apples = new Dictionary<Point, int>();
		private List<Point> _newlyRemovedApples = new List<Point>();
		private List<Point> _newlyAddedApples = new List<Point>();
		private ISnake _snake;
		private ConsoleColor _appleColor;
		private char _appleDrawingChar;

		public Apples(int minApples, int appleLifeTime, char appleDrawingChar, ConsoleColor appleColor, ISnake snake)
		{
			MinApples = minApples;
			AppleLifetime = appleLifeTime;
			_snake = snake;
			_appleColor = appleColor;
			_appleDrawingChar = appleDrawingChar;
			Console = _snake.Console;
		}

		public IConsoleWrapper Console { get; private set; }

		public int AppleLifetime { get; set; }

		public int MinApples { get; set; }

		public void Draw()
		{
			Console.ForegroundColor = _appleColor;
			foreach (var apple in _apples)
			{
				Console.SetCursorPosition(apple.Key.X, apple.Key.Y);
				Console.Write(_appleDrawingChar);
			}
			Console.ForegroundColor = Config.DefaultConsoleForeground;
			_newlyAddedApples.Clear();
		}

		public void Redraw()
		{
			if (!NeedsRedraw()) 
				return;

			Console.ForegroundColor = _appleColor;
			foreach (var apple in _newlyRemovedApples)
			{
				Console.SetCursorPosition(apple.X, apple.Y);
				Console.Write(Config.NullChar);
			}
			foreach (var apple in _newlyAddedApples)
			{
				Console.SetCursorPosition(apple.X, apple.Y);
				Console.Write(_appleDrawingChar);
			}
			Console.ForegroundColor = Config.DefaultConsoleForeground;
			_newlyRemovedApples.Clear();
			_newlyAddedApples.Clear();
		}

		public bool NeedsRedraw()
		{
			return _newlyRemovedApples.Count > 0 || _newlyAddedApples.Count > 0;
		}

		public void RemoveOldApplesAndAddNewIfNeeded(int currentGameTick)
		{
			foreach (var apple in _apples.ToList())
			{
				if (currentGameTick - apple.Value > AppleLifetime)
				{
					_apples.Remove(apple.Key);
					_newlyRemovedApples.Add(apple.Key);
				}
			}
	
			if (_apples.Count >= MinApples)
				return;

			var rand = new Random();
			for (int i = 0; i < MinApples - _apples.Count; i++)
			{
				var newApple = new Point(rand.Next(1, Console.WindowWidth - 2), rand.Next(1, Console.WindowHeight - 3));
				if (_apples.ContainsKey(newApple) || _snake.ContainsPosition(newApple))
					break;
				_apples.Add(newApple, currentGameTick);
				_newlyAddedApples.Add(newApple);
			}
		}

		public bool HasAppleAt(Point position)
		{
			return _apples.ContainsKey(position);
		}

		public void EatAppleAt(Point position)
		{
			_apples.Remove(position);
			_newlyRemovedApples.Add(position);
		}
	}
}
