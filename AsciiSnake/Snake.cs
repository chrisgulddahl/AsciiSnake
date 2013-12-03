using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;

namespace dk.ChrisGulddahl.AsciiSnake
{
	public class Snake : ISnake
	{
		private Direction _direction;
		private LinkedList<Point> _positions = new LinkedList<Point>();
		private bool _hasGrown = false;

		public Snake(ICanvas canvas, IConfig config, int startX, int startY)
		{
			Canvas = canvas;
			Config = config;
			Direction = Direction.None;
			Crashed = false;
			_positions.AddLast(new Point(startX, startY));
		}

		private ICanvas Canvas { get; set; }

		private IConfig Config { get; set; }

		public bool Crashed { get; set; }

		public Direction Direction
		{
			get { return _direction; }
			set
			{
				if (!OppositeDirections(_direction, value))
				{
					_direction = value;
				}
			}
		}

		public int Length
		{
			get { return _positions.Count; }
		}

		public Point Head
		{
			get { return _positions.First.Value; }
		}

		public void Draw()
		{
			if (!NeedsRedraw())
				return;
			var snakeColor = Config.SnakeColor;
			var snakeBodyChar = Config.SnakeBodyDrawingChar;
			Canvas.DrawChar(Head, Config.SnakeHeadDrawingChar, snakeColor);
			var elem = _positions.First.Next;
			while (elem != null)
			{
				Canvas.DrawChar(elem.Value, snakeBodyChar, snakeColor);
				elem = elem.Next;
			}
		}

		public bool NeedsRedraw()
		{
			return true;
		}

		public void Move()
		{
			if (Direction == Direction.None)
				return;

			Point oldHead = Head;
			if (!_hasGrown)
			{
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

		private bool OppositeDirections(Direction dir1, Direction dir2)
		{
			return ((int)dir1 + (int)dir2) == 0;
		}
	}
}
