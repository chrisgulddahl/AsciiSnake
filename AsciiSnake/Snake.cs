using System;
using System.Collections.Generic;
using System.Drawing;

namespace dk.ChrisGulddahl.AsciiSnake
{
	class Snake : ISnake
	{
		private Direction _direction;
		private LinkedList<Point> _positions = new LinkedList<Point>();
		private List<Point> _newlyChangedPositions = new List<Point>();
		private List<Point> _newlyRemovedPositions = new List<Point>();
		private char _snakeBodyDrawingChar;
		private char _snakeHeadDrawingChar;
		private bool _hasGrown = false;
		private ConsoleColor _snakeColor;

		public Snake(IConsoleWrapper console, int startX, int startY, char snakeHeadDrawingChar, char snakeBodyDrawingChar, ConsoleColor snakeColor)
		{
			Console = console;
			Direction = Direction.None;
			_positions.AddLast(new Point(startX, startY));
			_snakeBodyDrawingChar = snakeBodyDrawingChar;
			_snakeHeadDrawingChar = snakeHeadDrawingChar;
			_snakeColor = snakeColor;
		}

		public IConsoleWrapper Console { get; private set; }

		public Direction Direction
		{
			get { return _direction; }
			set
			{
				if (!OppositeDirections(_direction, value))
					_direction = value;
			}
		}

		public int Length
		{
			get { return _positions.Count; }
		}

		private bool OppositeDirections(Direction dir1, Direction dir2)
		{
			if (dir1 == Direction.North)
				return dir2 == Direction.South;
			else if (dir1 == Direction.South)
				return dir2 == Direction.North;
			else if (dir1 == Direction.East)
				return dir2 == Direction.West;
			else if (dir1 == Direction.West)
				return dir2 == Direction.East;
			return false;
		}

		public Point Head
		{
			get { return _positions.First.Value; }
		}

		public void Draw()
		{
			DrawSnakeWithChar(_snakeHeadDrawingChar, _snakeBodyDrawingChar);
			_newlyChangedPositions.Clear();
		}

		public void Redraw()
		{
			if (!NeedsRedraw()) 
				return;

			Console.ForegroundColor = _snakeColor;
			// Erase removed elements
			foreach(var position in _newlyRemovedPositions)
				DrawSnakePoint(position, Config.NullChar);

			// Update changed elements
			foreach (var position in _newlyChangedPositions)
				DrawSnakePoint(position, (position == Head ? _snakeHeadDrawingChar : _snakeBodyDrawingChar));

			Console.ForegroundColor = Config.DefaultConsoleForeground;
			_newlyRemovedPositions.Clear();
			_newlyChangedPositions.Clear();
		}

		public bool NeedsRedraw()
		{
			return _newlyChangedPositions.Count > 0 || _newlyRemovedPositions.Count > 0;
		}

		public void Move()
		{
			if (Direction == Direction.None)
				return;

			Point oldHead = Head;
			if (!_hasGrown)
			{
				_newlyRemovedPositions.Add(_positions.Last.Value);
				_positions.RemoveLast();
			}
			switch (Direction)
			{
				case Direction.North: _positions.AddFirst(new Point(oldHead.X, oldHead.Y - 1));
					break;
				case Direction.South: _positions.AddFirst(new Point(oldHead.X, oldHead.Y + 1));
					break;
				case Direction.West: _positions.AddFirst(new Point(oldHead.X - 1, oldHead.Y));
					break;
				case Direction.East: _positions.AddFirst(new Point(oldHead.X + 1, oldHead.Y));
					break;
			}
			_newlyChangedPositions.Add(Head);
			if (ContainsPosition(oldHead))
				_newlyChangedPositions.Add(oldHead);
			_hasGrown = false;
		}

		public void Grow()
		{
			_hasGrown = true;
		}

		public bool CrashedWithSelf()
		{
			var elem = _positions.First.Next;
			while (elem != null)
				if (elem.Value == Head)
					return true;
				else
					elem = elem.Next;
			return false;
		}

		public bool ContainsPosition(Point position)
		{
			return _positions.Contains(position);
		}

		private void DrawSnakeWithChar(char snakeHeadChar, char snakeBodyChar)
		{
			Console.ForegroundColor = _snakeColor;
			Console.SetCursorPosition(_positions.First.Value.X, _positions.First.Value.Y);
			Console.Write(snakeHeadChar);
			var elem = _positions.First.Next;
			while (elem != null)
			{
				Console.SetCursorPosition(elem.Value.X, elem.Value.Y);
				Console.Write(snakeBodyChar);
				elem = elem.Next;
			}
			Console.ForegroundColor = Config.DefaultConsoleForeground;
		}

		private void DrawSnakePoint(Point position, char snakeChar)
		{
			Console.SetCursorPosition(position.X, position.Y);
			Console.Write(snakeChar);
		}
	}
}
