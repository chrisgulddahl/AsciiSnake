﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;

namespace dk.ChrisGulddahl.AsciiSnake
{
	class Game : IGame
	{
		private IGameScore _score;
		private IBorder _border;
		private ISnake _snake;
		private IApples _apples;
		private ISoundManager _soundManager;
		private IDrawable _drawables;
		private bool _quit;
		private bool _crashed;
		private Thread _gameThread;

		public Game(IConfig config, IConsoleWrapper console, IDiffFlushableCanvas canvas, IGameScore score, ISoundManager soundManager, IBorder border, ISnake snake, IApples apples)
		{
			Config = config;
			Console = console;
			Canvas = canvas;
			_score = score;
			_soundManager = soundManager;
			_border = border;
			_snake = snake;
			_apples = apples;
			_drawables = new DrawableCollection(new List<IDrawable> { _border, _apples, _snake });
			Reset();
		}

		public int CurrentTick { get; private set; }

		private IDiffFlushableCanvas Canvas { get; set; }

		private IConsoleWrapper Console { get; set; }

		private IConfig Config { get; set; }

		private void Reset()
		{
			_border.Reset();
			_snake.Reset();
			_apples.Reset();
			Console.OutputEncoding = Encoding.ASCII;
			Console.CursorVisible = false;
			Console.BackgroundColor = Config.ConsoleBackground;
			Console.ForegroundColor = Config.ConsoleForeground;
			Console.Title = "ASCII Snake by chrisgulddahl.dk";
			Console.Clear();
			CurrentTick = 0;
			_quit = false;
			_crashed = false;
		}

		public void Start()
		{
			_gameThread = new Thread(Run);
			_gameThread.Start();
		}

		private void Run()
		{
			// Draw game for the first time
			_apples.RefreshApples(CurrentTick);
			_drawables.Draw(Canvas);
			Canvas.WriteCurrent();

			// Wait for player to press a key
			while (!Console.KeyAvailable)
			{
				Thread.Sleep(10);
				_drawables.Draw(Canvas);
				Canvas.FlushChanges();
				Console.RefreshWindowDimensions();
			}

			// Main game loop
			while (!_quit && !_crashed)
			{
				var start = DateTime.Now.Ticks;
				Tick();
				int elapsedTimeMs = (int)(DateTime.Now.Ticks - start) / 10000;
				Thread.Sleep((Config.TickTime - elapsedTimeMs >= 0 ? Config.TickTime - elapsedTimeMs : 0));
			}

			// Decide whether to restart or quit
			if (_crashed)
			{
				_soundManager.PlayCrashedSound();
				DisplayCrashedMessage(new string[]{"You crashed!",
				                      "Final score: " + _score.Value,
				                      "Press Q to quit. Press any other key to restart"});
				while (!Console.KeyAvailable)
					Thread.Sleep(10);
				if (Console.ReadKey(true).KeyChar == 'q')
					return;

				Reset();
				Start();
			}
		}

		private void Tick()
		{
			HandleKeyPress();
			_snake.Move();
			HandleSnakeEatingApple();
			if (_border.ContainsPosition(_snake.Head) || _snake.CrashedWithSelf())
			{
				_crashed = true;
				return;
			}
			_apples.RefreshApples(CurrentTick);
			_drawables.Draw(Canvas);
			Canvas.FlushChanges();
			CurrentTick++;
		}

		private void HandleKeyPress()
		{
			if (!Console.KeyAvailable)
				return;

			switch (Console.ReadKey(true).KeyChar)
			{
				case 'w': _snake.Direction = Direction.North;
					break;
				case 'a': _snake.Direction = Direction.West;
					break;
				case 's': _snake.Direction = Direction.South;
					break;
				case 'd': _snake.Direction = Direction.East;
					break;
				case 'q': _quit = true;
					break;
				case 'm': _soundManager.Muted = !_soundManager.Muted;
					break;
			}
		}

		private void HandleSnakeEatingApple()
		{
			if (_apples.HasAppleAt(_snake.Head))
			{
				_soundManager.PlayEatAppleSound();
				_apples.RemoveAppleAt(_snake.Head);
				_snake.Grow();
			}
		}

		private void DisplayCrashedMessage(params string[] lines)
		{
			int messageBoxWidth = lines.Aggregate(0, (maxLength, line) => (Math.Max(line.Length, maxLength)));
			int messageBoxHeight = lines.Count();
			int messageBoxLeft = (Console.WindowWidth - messageBoxWidth) / 2;
			int messageBoxTop = (Console.WindowHeight - messageBoxHeight) / 2;
			for (int i = 0; i < lines.Count(); i++)
			{
				Canvas.DrawString(lines[i], new Point(messageBoxLeft + (messageBoxWidth - lines.ElementAt(i).Length) / 2, messageBoxTop + i), Direction.East,  Config.ConsoleForeground);
			}
			Canvas.WriteCurrent();
		}
	}
}
