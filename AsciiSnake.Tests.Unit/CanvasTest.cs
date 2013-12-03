using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;

namespace dk.ChrisGulddahl.AsciiSnake.Tests.Unit
{
	[TestFixture]
	class CanvasTest
	{
		private IDiffableCanvas _uutDiffableCanvas;
		private IConsoleWrapper _mockConsole;
		private IConfig _config;
		private MockRepository _mocks;

		[SetUp]
		public void SetUp()
		{
			_mocks = new MockRepository();
			_config = new DefaultConfig();
			_mockConsole = _mocks.DynamicMock<IConsoleWrapper>();
			_uutDiffableCanvas = new DiffableCanvas(_mockConsole, _config);
		}

		[Test]
		public void DefaultCtor_fresh_empty()
		{
			var canvasCharCount = _uutDiffableCanvas.Count();
			Assert.AreEqual(0, canvasCharCount);
		}

		/*[Test]
		public void DrawChar_fresh_containsChar()
		{
			_uutCanvas.DrawChar(new Point(2, 3), 'Z', ConsoleColor.Cyan);
			var charAtPos = _uutCanvas.TopCharAtPos(new Point(2, 3));
			Assert.IsNotNull(charAtPos);
			Assert.AreEqual(new Point(2, 3), charAtPos.Position);
			Assert.AreEqual(ConsoleColor.Cyan, charAtPos.Color);
			Assert.AreEqual('Z', charAtPos.Character);
		}*/

		[Test]
		public void WriteToConsole_charsAdded_consoleCalled()
		{
			_uutDiffableCanvas.DrawChar(new Point(2, 3), 'Z');
			_uutDiffableCanvas.DrawChar(new Point(7, 0), '$', ConsoleColor.Blue);
			using (_mocks.Ordered())
			{
				Expect.Call(() => _mockConsole.ForegroundColor = Arg<ConsoleColor>.Is.Anything).Repeat.Times(0, 1);
				Expect.Call(() => _mockConsole.SetCursorPosition(2, 3));
				Expect.Call(() => _mockConsole.Write('Z'));
			}
			using (_mocks.Ordered())
			{
				using (_mocks.Unordered())
				{
					Expect.Call(() => _mockConsole.ForegroundColor = ConsoleColor.Blue).Repeat.Once();
					Expect.Call(() => _mockConsole.SetCursorPosition(7, 0));
				}
				Expect.Call(() => _mockConsole.Write('$'));
			}
			_mocks.ReplayAll();
			_uutDiffableCanvas.WriteToConsole();
			_mocks.VerifyAll();
		}

		[Test]
		public void WriteToConsole_twoCharsOnSamePosition_onlyOneWrittenToConsole()
		{
			_uutDiffableCanvas.DrawChar(new Point(5, 3), 'Z');
			_uutDiffableCanvas.DrawChar(new Point(5, 3), '$');

			using (_mocks.Ordered())
			{
				Expect.Call(() => _mockConsole.ForegroundColor = Arg<ConsoleColor>.Is.Anything).Repeat.Times(0, 1);
				Expect.Call(() => _mockConsole.SetCursorPosition(5,3));
				Expect.Call(() => _mockConsole.Write('$'));
			}
			DoNotExpect.Call(()=>_mockConsole.Write('Z'));

			_mocks.ReplayAll();
			_uutDiffableCanvas.WriteToConsole();
			_mocks.VerifyAll();
		}

		[Test]
		public void WriteToConsole_emptyCanvas_noCallsToConsole()
		{
			DoNotExpect.Call(() => _mockConsole.SetCursorPosition(Arg<int>.Is.Anything, Arg<int>.Is.Anything));
			DoNotExpect.Call(() => _mockConsole.ForegroundColor = Arg<ConsoleColor>.Is.Anything);
			DoNotExpect.Call(() => _mockConsole.Write(Arg<char>.Is.Anything));
			_mocks.ReplayAll();
			_uutDiffableCanvas.WriteToConsole();
			_mocks.VerifyAll();
		}

		[Test]
		public void Diff_2newChars_bothWrittenToConsole()
		{
			_uutDiffableCanvas.DrawChar(new Point(1, 1), 'a');
			_uutDiffableCanvas.DrawChar(new Point(2, 2), 'b');
			var canvas2 = new DiffableCanvas(_mockConsole, _config);
			canvas2.DrawChar(new Point(1, 1), 'a');//unchanged
			canvas2.DrawChar(new Point(2, 2), 'b');//unchanged
			canvas2.DrawChar(new Point(4, 1), 'c');//new chars
			canvas2.DrawChar(new Point(5, 2), 'd');//new chars
			using (_mocks.Ordered())
			{
				Expect.Call(() => _mockConsole.ForegroundColor = Arg<ConsoleColor>.Is.Anything).Repeat.Times(0, 1);
				Expect.Call(() => _mockConsole.SetCursorPosition(4, 1));
				Expect.Call(() => _mockConsole.Write('c'));
			}
			using (_mocks.Ordered())
			{
				Expect.Call(() => _mockConsole.ForegroundColor = Arg<ConsoleColor>.Is.Anything).Repeat.Times(0, 1);
				Expect.Call(() => _mockConsole.SetCursorPosition(5,2));
				Expect.Call(() => _mockConsole.Write('d'));
			}
			DoNotExpect.Call(() => _mockConsole.SetCursorPosition(1,1));
			DoNotExpect.Call(() => _mockConsole.SetCursorPosition(2,2));
			DoNotExpect.Call(() => _mockConsole.Write('a'));
			DoNotExpect.Call(() => _mockConsole.Write('b'));
			_mocks.ReplayAll();
			var diffCanvas = _uutDiffableCanvas.Diff(canvas2);
			diffCanvas.WriteToConsole();
			_mocks.VerifyAll();
		}

		[Test]
		public void Diff_2charsRemoved_bothOverwrittenWithNullChars()
		{
			_uutDiffableCanvas.DrawChar(new Point(1, 1), 'a');
			_uutDiffableCanvas.DrawChar(new Point(2, 2), 'b');
			_uutDiffableCanvas.DrawChar(new Point(4, 1), 'c');
			_uutDiffableCanvas.DrawChar(new Point(5, 2), 'd');
			var canvas2 = new DiffableCanvas(_mockConsole, _config);
			canvas2.DrawChar(new Point(4, 1), 'c');
			canvas2.DrawChar(new Point(5, 2), 'd');
			using (_mocks.Ordered())
			{
				Expect.Call(() => _mockConsole.ForegroundColor=Arg<ConsoleColor>.Is.Anything).Repeat.Times(0,1);
				Expect.Call(() => _mockConsole.SetCursorPosition(1,1));
				Expect.Call(() => _mockConsole.Write(_config.NullChar));
			}
			using (_mocks.Ordered())
			{
				Expect.Call(() => _mockConsole.ForegroundColor = Arg<ConsoleColor>.Is.Anything).Repeat.Any();
				Expect.Call(() => _mockConsole.SetCursorPosition(2,2));
				Expect.Call(() => _mockConsole.Write(_config.NullChar));
			}
			DoNotExpect.Call(() => _mockConsole.SetCursorPosition(4, 1));
			DoNotExpect.Call(() => _mockConsole.SetCursorPosition(5,2));
			DoNotExpect.Call(() => _mockConsole.Write('c'));
			DoNotExpect.Call(() => _mockConsole.Write('d'));
			_mocks.ReplayAll();
			var diffCanvas = _uutDiffableCanvas.Diff(canvas2);
			diffCanvas.WriteToConsole();
			_mocks.VerifyAll();
		}

		[Test]
		public void Diff_charReplaced_overwritten()
		{
			_uutDiffableCanvas.DrawChar(new Point(5, 2), 'a', ConsoleColor.Gray);
			var canvas2 = new DiffableCanvas(_mockConsole, _config);
			canvas2.DrawChar(new Point(5, 2), 'b', ConsoleColor.Green);
			using (_mocks.Ordered())
			{
				using (_mocks.Unordered())
				{
					Expect.Call(() => _mockConsole.SetCursorPosition(5, 2));
					Expect.Call(() => _mockConsole.ForegroundColor = ConsoleColor.Green);
				}
				Expect.Call(() => _mockConsole.Write('b'));
			}
			DoNotExpect.Call(() => _mockConsole.Write(_config.NullChar));
			_mocks.ReplayAll();
			var diffCanvas = _uutDiffableCanvas.Diff(canvas2);
			diffCanvas.WriteToConsole();
			_mocks.VerifyAll();
		}

		/*[Test]
		public void Clone_nonEmpty_returnsShallowClone()
		{
			_uutCanvas.DrawChar(new Point(1, 1), 'a');
			_uutCanvas.DrawChar(new Point(2, 2), 'b');
			var clone = (Canvas)_uutCanvas.Clone();
			Assert.AreEqual(2, clone.Count());
			var pos1Char = clone.TopCharAtPos(new Point(1, 1));
			var pos2Char = clone.TopCharAtPos(new Point(2, 2));
			Assert.AreEqual('a', pos1Char.Character);
			Assert.AreEqual('b', pos2Char.Character);
		}*/
	}
}
