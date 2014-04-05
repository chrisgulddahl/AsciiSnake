using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dk.ChrisGulddahl.AsciiSnake
{
	public class SnakeLengthGameScore : IGameScore
	{
		private ISnake _snake;
		public SnakeLengthGameScore(ISnake snake)
		{
			_snake = snake;
		}

		public int Value
		{
			get { return _snake.Length - 1; }
		}
	}
}
