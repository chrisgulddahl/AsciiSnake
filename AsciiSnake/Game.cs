using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;

namespace dk.ChrisGulddahl.AsciiSnake
{
	class Game : IGame
	{
		private readonly IGameFactory _factory;
		private IBorder _border;
		private ISnake _snake;
		private IApples _apples;
		private ISoundManager _soundManager;
		private IDrawable _drawables;
		private bool _quit;
		private bool _crashed;
		private Thread _gameThread;

		public Game(IGameFactory factory)
		{
			_factory = factory;
			Reset();
		}

		public int CurrentTick { get; private set; }

		private IDiffFlushableCanvas Canvas { get; set; }

		public int Score
		{
			get { return _snake.Length - 1; }
		}

		private IConsoleWrapper Console { get; set; }

		private IConfig Config { get; set; }

		private void Reset()
		{
			//Remember state
			var isMuted = (_soundManager != null && _soundManager.Muted);

			//Reset
			Config = _factory.Config;
			Console = _factory.Console;
			Canvas = _factory.DiffFlushableCanvas;
			_border = _factory.GetBorder(this);
			_snake = _factory.GetSnake(Console);
			_apples = _factory.GetApples(_snake);
			_soundManager = _factory.GetSoundManager();
			_soundManager.Muted = isMuted;
			_drawables = new CompositeDrawable(new List<IDrawable> { _border, _apples, _snake });
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
			_drawables.Draw();
			Canvas.WriteCurrent();

			// Wait for player to press a key
			while (!Console.KeyAvailable)
			{
				Thread.Sleep(10);
				_drawables.Draw();
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
				                      "Final score: " + Score,
				                      "Press Q to quit. Press any other key to restart"});
				while (!Console.KeyAvailable)
					Thread.Sleep(10);
				if (Console.ReadKey(true).KeyChar == 'q')
					return;

				Reset();
				Start();
			}
		}

		public void Tick()
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
			_drawables.Draw();
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
