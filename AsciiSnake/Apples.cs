﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace dk.ChrisGulddahl.AsciiSnake
{
	public class Apples : IApples
	{
		private readonly Dictionary<Point, int> _apples = new Dictionary<Point, int>();
		private readonly ISnake _snake;

		public Apples(IConfig config, ISnake snake, INewAppleLocationStrategy newAppleLocationStrategy)
		{
			Config = config;
			_snake = snake;
			NewAppleLocationStrategy = newAppleLocationStrategy;
		}

		private IConfig Config { get; set; }

		public INewAppleLocationStrategy NewAppleLocationStrategy { get; set; }

		public void Draw(ICanvas canvas)
		{
			var c = Config.AppleDrawingChar;
			var color = Config.AppleColor;
			foreach (var apple in _apples)
				canvas.DrawChar(apple.Key, c, color);
		}

		public void RefreshApples(int currentGameTick)
		{
			foreach (var apple in _apples.ToList())
			{
				if (currentGameTick - apple.Value > Config.AppleLifetime)
					RemoveAppleAt(apple.Key);
			}

			if (_apples.Count >= Config.MinAppleCount)
				return;

			var addedApples = new List<Point>();
			for (int i = 0; i < Config.MinAppleCount - _apples.Count; i++)
			{
				var newApple = NewAppleLocationStrategy.GetNew();
				if (_apples.ContainsKey(newApple) || addedApples.Contains(newApple) ||
					_snake.ContainsPosition(newApple))
				{
					continue;
				}
				addedApples.Add(newApple);
			}
			foreach (var apple in addedApples)
			{
				_apples.Add(apple, currentGameTick);
			}
		}

		public bool HasAppleAt(Point position)
		{
			return _apples.ContainsKey(position);
		}

		public void RemoveAppleAt(Point position)
		{
			_apples.Remove(position);
		}

		public void Reset()
		{
			_apples.Clear();
		}
	}
}
