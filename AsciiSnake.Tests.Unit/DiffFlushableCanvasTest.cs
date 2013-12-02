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
	public class DiffFlushableCanvasTest
	{
		private IDiffFlushableCanvas _diffFlushableCanvas;
		private IConfig _config;
		private IConsoleWrapper _mockConsole;
		private MockRepository _mocks;

		[SetUp]
		public void Setup()
		{
			_mocks = new MockRepository();
			_config = new DefaultConfig();
			_mockConsole = _mocks.DynamicMock<IConsoleWrapper>();
			_diffFlushableCanvas = new DiffFlushableCanvas(_mockConsole, _config);
		}

		[Test]
		public void FlushChangesToConsole_NothingDrawn_NothingWrittenToConsole()
		{
			DoNotExpect.Call(()=>_mockConsole.Write(Arg<char>.Is.Anything));
			_mocks.ReplayAll();
			_diffFlushableCanvas.FlushChangesToConsole();
			_mocks.VerifyAll();
		}

		[Test]
		public void FlushChangesToConsole_NewCharsDrawn_CharsWrittenToConsole()
		{
			using (_mocks.Ordered())
			{
				using (_mocks.Unordered())
				{
					_mockConsole.SetCursorPosition(0, 0);
					_mockConsole.ForegroundColor = ConsoleColor.Magenta;
				}
				_mockConsole.Write('a');
			}
			using (_mocks.Ordered())
			{
				_mockConsole.SetCursorPosition(4, 23);
				_mockConsole.Write('b');
			}
			_mocks.ReplayAll();
			_diffFlushableCanvas.DrawChar(new Point(0, 0), 'a', ConsoleColor.Magenta);
			_diffFlushableCanvas.DrawChar(new Point(4, 23), 'b');
			_diffFlushableCanvas.FlushChangesToConsole();
			_mocks.VerifyAll();
		}

		[Test]
		public void FlushChangesToConsole_UnchangedChars_NothingWrittenToConsole()
		{
			_diffFlushableCanvas.DrawChar(new Point(0, 0), 'a');
			_diffFlushableCanvas.DrawChar(new Point(4, 23), 'b');
			_diffFlushableCanvas.FlushChangesToConsole();

			_mocks.BackToRecordAll();
			using (_mocks.Record())
			{
				DoNotExpect.Call(() => _mockConsole.Write(Arg<char>.Is.Anything));
			}
			_mocks.ReplayAll();
			_diffFlushableCanvas.DrawChar(new Point(0, 0), 'a'); //Write same chars
			_diffFlushableCanvas.DrawChar(new Point(4, 23), 'b');//Write same chars
			_diffFlushableCanvas.FlushChangesToConsole(); //Should not cause anything to be written since, the chars are already flushed to console
			_mocks.VerifyAll();
		}

		[Test]
		public void FlushChangesToConsole_ChangedChar_NewCharWrittenToConsole()
		{
			_diffFlushableCanvas.DrawChar(new Point(4, 23), 'b');
			_diffFlushableCanvas.FlushChangesToConsole();

			_mocks.BackToRecordAll();
			using (_mocks.Ordered())
			{
				_mockConsole.SetCursorPosition(4,23);
				_mockConsole.Write('c');
			}
			_mocks.ReplayAll();
			_diffFlushableCanvas.DrawChar(new Point(4, 23), 'c');
			_diffFlushableCanvas.FlushChangesToConsole();
			_mocks.VerifyAll();
		}


		[Test]
		public void FlushChangesToConsole_RemovedChar_NullCharWrittenToConsole()
		{
			_diffFlushableCanvas.DrawChar(new Point(4, 23), 'b');
			_diffFlushableCanvas.FlushChangesToConsole();

			_mocks.BackToRecordAll();
			using (_mocks.Ordered())
			{
				_mockConsole.SetCursorPosition(4, 23);
				_mockConsole.Write(_config.NullChar);
			}
			_mocks.ReplayAll();
			_diffFlushableCanvas.FlushChangesToConsole();
			_mocks.VerifyAll();
		}

		[Test]
		public void ClearChangesToCanvas_NewChar_NoCallsToConsole()
		{
			using (_mocks.Record())
			{
				DoNotExpect.Call(() => _mockConsole.Write(Arg<char>.Is.Anything));
			}
			_mocks.ReplayAll();
			_diffFlushableCanvas.DrawChar(new Point(4, 23), 'b');
			_diffFlushableCanvas.ClearChangesToCanvas();
			_diffFlushableCanvas.FlushChangesToConsole();
			_mocks.VerifyAll();
		}

		[Test]
		public void WriteCurrentToConsole_ChangedChars_RewrittenToConsole()
		{
			_diffFlushableCanvas.DrawChar(new Point(0, 0), 'a');
			_diffFlushableCanvas.DrawChar(new Point(4, 23), 'b');
			_diffFlushableCanvas.FlushChangesToConsole();

			_mocks.BackToRecordAll();
			using (_mocks.Ordered())
			{
				_mockConsole.SetCursorPosition(0,0);
				_mockConsole.Write('a');
				_mockConsole.SetCursorPosition(4, 23);
				_mockConsole.Write('c');
			}
			_mocks.ReplayAll();
			_diffFlushableCanvas.DrawChar(new Point(0, 0), 'a');
			_diffFlushableCanvas.DrawChar(new Point(4, 23), 'c');
			_diffFlushableCanvas.WriteCurrentToConsole();
			_mocks.VerifyAll();
		}
	}
}
