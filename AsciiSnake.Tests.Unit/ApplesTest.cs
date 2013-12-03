using System.Drawing;
using NUnit.Framework;
using Rhino.Mocks;

namespace dk.ChrisGulddahl.AsciiSnake.Tests.Unit
{
	[TestFixture]
	class ApplesTest
	{
		private IApples _uutApples;
		private IDiffFlushableCanvas _mockCanvas;
		private ISnake _mockSnake;
		private INewAppleLocationStrategy _mockNewAppleLocationStrategy;
		private IConfig _config;

		[SetUp]
		public void Setup()
		{
			_config = new DefaultConfig();
			_mockNewAppleLocationStrategy = MockRepository.GenerateMock<INewAppleLocationStrategy>();
			_mockSnake = MockRepository.GenerateMock<ISnake>();
			_mockCanvas = MockRepository.GenerateMock<IDiffFlushableCanvas>();
			_uutApples = new Apples(_mockCanvas, _config, _mockSnake, _mockNewAppleLocationStrategy);
		}

		// Helper methods
		public void Helper_AttemptToAddApple(Point apple, int currentGameTick)
		{
			_mockNewAppleLocationStrategy = MockRepository.GenerateMock<INewAppleLocationStrategy>();
			_mockNewAppleLocationStrategy.Stub(x => x.GetNew()).Return(apple);
			_mockSnake.Stub(x => x.ContainsPosition(Arg<Point>.Is.Anything)).Return(false);
			_uutApples.NewAppleLocationStrategy = _mockNewAppleLocationStrategy;
			_uutApples.RefreshApples(currentGameTick);
		}


		// Tests
		[Test]
		public void RemoveOldApplesAndAddNewIfNeeded_AppleAdded_HasAppleAtReturnsTrue()
		{
			Helper_AttemptToAddApple(new Point(4, 7), 0);
			bool hasApple = _uutApples.HasAppleAt(new Point(4, 7));
			Assert.IsTrue(hasApple);
		}

		[Test]
		public void RemoveOldApplesAndAddNewIfNeeded_AppleAddedSomewhereElse_HasAppleAtReturnsFalse()
		{
			Helper_AttemptToAddApple(new Point(4, 7), 0);
			bool hasApple = _uutApples.HasAppleAt(new Point(4, 6));
			Assert.IsFalse(hasApple);
		}

		[Test]
		public void RemoveAppleAt_AppleAtPosition_HasAppleAtReturnsFalse()
		{
			Helper_AttemptToAddApple(new Point(4, 7), 0);
			_uutApples.RemoveAppleAt(new Point(4, 7));
			bool hasApple = _uutApples.HasAppleAt(new Point(4, 7));
			Assert.IsFalse(hasApple);
		}

		[Test]
		public void RemoveOldApplesAndAddNewIfNeeded_HasOldApple_OldAppleRemoved()
		{
			_config.AppleLifetime = 2;
			Helper_AttemptToAddApple(new Point(4, 7), 0); //Apple added at gametick 0
			Helper_AttemptToAddApple(new Point(3, 6), 3); //Apple at (4,7) should be removed at gametick 3
			bool hasApple = _uutApples.HasAppleAt(new Point(4, 7));
			Assert.IsFalse(hasApple);
		}

		[Test]
		public void Draw_SingleApple_CallsToConsole()
		{
			Helper_AttemptToAddApple(new Point(1, 2), 0);
			_uutApples.Draw();
			_mockCanvas.AssertWasCalled(x => x.DrawChar(new Point(1, 2), _config.AppleDrawingChar, _config.AppleColor));
		}
	}
}
