using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace dk.ChrisGulddahl.AsciiSnake
{
	public enum Direction
	{
		North,
		East,
		South,
		West,
		None
	}

	class Game : IGame
	{
		private IGameFactory _factory;
		private IConsoleWrapper _console;
		private IBorder _border;
		private ISnake _snake;
		private IApples _apples;
		private ISoundManager _soundManager;
		private IDrawable _drawables;
		private bool _quit;

		public Game(IGameFactory factory)
		{
			_factory = factory;
			Reset();
		}

		public int Tick { get; private set; }

		public int Score 
		{
			get { return _snake.Length; }
		}

		public IConsoleWrapper Console
		{
			get { return _console; }
		}

		private void Reset()
		{
			_console = _factory.CreateConsole();
			_border = _factory.CreateBorder(this);
			_snake = _factory.CreateSnake(Console);
			_apples = _factory.CreateApples(_snake);
			_soundManager = _factory.CreateSoundManager();
			_drawables = new CompositeDrawable(new List<IDrawable> { _border, _apples, _snake });
			Console.CursorVisible = false;
			Console.BackgroundColor = ConsoleColor.White;
			Console.ForegroundColor = Config.DefaultConsoleForeground;
			Console.Title = "ASCII Snake by chrisgulddahl.dk";
			Console.Clear();
			Tick = 0;
			_quit = false;
		}

		public void Start()
		{
			// Draw game for the first time
			_drawables.Draw();
			// Wait for player to press a key
			while (!Console.KeyAvailable)
			{
				Thread.Sleep(10);
				Console.RefreshWindowDimensions();
				_drawables.Redraw();
			}

			// Main game loop
			while (!_quit)
			{
				HandleKeyPress();
				_snake.Move();
				HandleSnakeEatingApple();
				if (_border.ContainsPosition(_snake.Head))
					break;
				if (_snake.CrashedWithSelf())
					break;
				_apples.RemoveOldApplesAndAddNewIfNeeded(Tick);
				_drawables.Redraw();
				Tick++;
				Thread.Sleep(120);
			}

			// If snake crash caused breaking main loop
			if (!_quit)
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

		private void DisplayCrashedMessage()
		{
			var lines = new List<string>
				{
					"You crashed!",
					"Final score: " + Score,
					"Press Q to quit. Press any other key to restart"
				};
			int messageBoxWidth = lines.Aggregate<string, int>(0, (maxLength, line) => (line.Length > maxLength ? line.Length : maxLength));
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
