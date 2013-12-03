using System;
using System.Collections.Generic;
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
		private IConfig _config;
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

		private void Reset()
		{
			//Remember state
			var isMuted = (_soundManager != null && _soundManager.Muted);

			//Reset
			_config = _factory.Config;
			Console = _factory.Console;
			Canvas = _factory.DiffFlushableCanvas;
			_border = _factory.CreateBorder(this);
			_snake = _factory.CreateSnake(Console);
			_apples = _factory.CreateApples(_snake);
			_soundManager = _factory.CreateSoundManager();
			_soundManager.Muted = isMuted;
			_drawables = new CompositeDrawable(new List<IDrawable> { _border, _apples, _snake });
			Console.OutputEncoding = Encoding.ASCII;
			Console.CursorVisible = false;
			Console.BackgroundColor = _config.ConsoleBackground;
			Console.ForegroundColor = _config.ConsoleForeground;
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
			Canvas.WriteCurrentToConsole();

			// Wait for player to press a key
			while (!Console.KeyAvailable)
			{
				Thread.Sleep(10);
				_drawables.Draw();
				Canvas.FlushChangesToConsole();
				Console.RefreshWindowDimensions();

			}
			while (!_quit && !_crashed)
			{
				var start = DateTime.Now.Ticks;
				Tick();
				int elapsedTimeMs = (int)(DateTime.Now.Ticks - start) / 10000;
				Thread.Sleep((_config.TickTime - elapsedTimeMs >= 0 ? _config.TickTime - elapsedTimeMs : 0));
			}

			if (_crashed)
			{
				_soundManager.PlayCrashedSound();
				DisplayCrashedMessage();
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
			Canvas.FlushChangesToConsole();
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

		private void DisplayCrashedMessage() //TODO: draw using canvas
		{
			var lines = new List<string>
				{
					"You crashed!",
					"Final score: " + Score,
					"Press Q to quit. Press any other key to restart"
				};
			int messageBoxWidth = lines.Aggregate<string, int>(0, (maxLength, line) => (Math.Max(line.Length, maxLength)));
			int messageBoxHeight = lines.Count;
			int messageBoxLeft = (Console.WindowWidth - messageBoxWidth) / 2;
			int messageBoxTop = (Console.WindowHeight - messageBoxHeight) / 2;
			for (int i = 0; i < lines.Count; i++)
			{
				Console.SetCursorPosition(messageBoxLeft + (messageBoxWidth - lines.ElementAt(i).Length) / 2, messageBoxTop + i);
				Console.Write(lines.ElementAt(i));
			}
		}
	}
}
