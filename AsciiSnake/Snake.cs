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
			_positions.AddLast(new Point(startX, startY));
		}

		private ICanvas Canvas { get; set; }

		private IConfig Config { get; set; }

		public Direction Direction
		{
			get { return _direction; }
			set
			{
				if (!DirectionUtil.AreOpposite(_direction, value))
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

		public void Move()
		{
			if (Direction == Direction.None)
				return;

			_positions.AddFirst(DirectionUtil.OffsetPosition(Head, Direction));
			if (!_hasGrown)
			{
				_positions.RemoveLast();
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
	}
}
