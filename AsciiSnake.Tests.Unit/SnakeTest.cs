using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;

namespace dk.ChrisGulddahl.AsciiSnake.Tests.Unit
{
	[TestFixture]
	class SnakeTest
	{
		private ISnake _uutSnake;
		private IDiffFlushableCanvas _mockCanvas;
		private IConfig _config;

		[SetUp]
		public void Setup()
		{
			_config = new DefaultConfig();
			_mockCanvas = MockRepository.GenerateMock<IDiffFlushableCanvas>();
			_uutSnake = new Snake(_mockCanvas, _config, 10, 15);
		}

		[Test]
		public void ContainsPosition_fresh_containsStartingPosition()
		{
			Assert.IsTrue(_uutSnake.ContainsPosition(new Point(10,15)));
		}

		[Test]
		public void GrowAndMove_grownTwice_containsNewPositions()
		{
			_uutSnake.Direction = Direction.South;
			_uutSnake.Grow();
			_uutSnake.Move();
			_uutSnake.Grow();
			_uutSnake.Move();
			Assert.IsTrue(_uutSnake.ContainsPosition(new Point(10, 15)));
			Assert.IsTrue(_uutSnake.ContainsPosition(new Point(10, 16)));
			Assert.IsTrue(_uutSnake.ContainsPosition(new Point(10, 17)));
		}

		[Test]
		public void Move_directionWest_movedSnake()
		{
			_uutSnake.Direction = Direction.West;
			_uutSnake.Move();
			Assert.IsFalse(_uutSnake.ContainsPosition(new Point(10, 15))); //removed tail
			Assert.IsTrue(_uutSnake.ContainsPosition(new Point(9, 15))); //added head
		}

		[Test]
		public void Move_noDirection_snakeNotMoved()
		{
			_uutSnake.Direction = Direction.None;
			_uutSnake.Move();
			Assert.IsTrue(_uutSnake.ContainsPosition(new Point(10, 15)));

			Assert.IsFalse(_uutSnake.ContainsPosition(new Point(9, 15)));
			Assert.IsFalse(_uutSnake.ContainsPosition(new Point(11, 15)));
			Assert.IsFalse(_uutSnake.ContainsPosition(new Point(10, 14)));
			Assert.IsFalse(_uutSnake.ContainsPosition(new Point(10, 16)));
		}

		[Test]
		public void Head_fresh_returnsStartingPosition()
		{
			Assert.AreEqual(new Point(10,15), _uutSnake.Head);
		}

		[Test]
		public void Head_snakeMoved_headMoved()
		{
			_uutSnake.Direction = Direction.East;
			_uutSnake.Move();
			Assert.AreEqual(new Point(11, 15), _uutSnake.Head);
		}

		[Test]
		public void Length_grownOnce_returns2()
		{
			_uutSnake.Direction = Direction.South;
			_uutSnake.Grow();
			_uutSnake.Move();
			Assert.AreEqual(2, _uutSnake.Length);
		}

		[Test]
		public void CrashedWithSelf_fresh_returnsFalse()
		{
			Assert.IsFalse(_uutSnake.CrashedWithSelf());
		}

		[Test]
		public void CrashedWithSelf_crashed_returnsTrue()
		{
			_uutSnake.Direction = Direction.North;
			_uutSnake.Grow();
			_uutSnake.Move();
			_uutSnake.Direction = Direction.East;
			_uutSnake.Grow();
			_uutSnake.Move();
			_uutSnake.Direction = Direction.South;
			_uutSnake.Grow();
			_uutSnake.Move();
			_uutSnake.Direction = Direction.West;
			_uutSnake.Grow();
			_uutSnake.Move();
			Assert.IsTrue(_uutSnake.CrashedWithSelf());
		}

		[Test]
		public void Draw_fresh_drawnIntialSnakeToCanvas()
		{
			_uutSnake.Draw();
			_mockCanvas.AssertWasCalled(x=>x.DrawChar(new Point(10,15), _config.SnakeHeadDrawingChar, _config.SnakeColor));
		}

		[Test]
		public void Draw_moved_drawnNewPosition()
		{
			_uutSnake.Direction = Direction.North;
			_uutSnake.Move();
			_uutSnake.Draw();
			_mockCanvas.AssertWasCalled(x => x.DrawChar(new Point(10, 14), _config.SnakeHeadDrawingChar, _config.SnakeColor)); //Drawn new position
			_mockCanvas.AssertWasNotCalled(x => x.DrawChar(new Point(10, 15), _config.SnakeHeadDrawingChar, _config.SnakeColor)); //Old position not drawn
		}

		[Test]
		public void Draw_grownOnce_drawnNewPosition()
		{
			_uutSnake.Direction = Direction.North;
			_uutSnake.Grow();
			_uutSnake.Move();
			_uutSnake.Draw();
			_mockCanvas.AssertWasCalled(x => x.DrawChar(new Point(10, 14), _config.SnakeHeadDrawingChar, _config.SnakeColor)); //New head drawn
			_mockCanvas.AssertWasCalled(x => x.DrawChar(new Point(10, 15), _config.SnakeBodyDrawingChar, _config.SnakeColor)); //Tail drawn
		}
	}
}
