using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace dk.ChrisGulddahl.AsciiSnake
{
	public struct CanvasChar : ICanvasChar
	{
		private Point _position;
		private char _character;
		private ConsoleColor _color;

		public CanvasChar(Point position, char character, ConsoleColor color)
		{
			_position = position;
			_character = character;
			_color = color;
		}

		public Point Position { 
			get { return _position; }
			set { _position = value; }
		}

		public char Character
		{
			get { return _character; }
			set { _character = value; }
		}

		public ConsoleColor Color
		{
			get { return _color; }
			set { _color = value; }
		}
	}
}
