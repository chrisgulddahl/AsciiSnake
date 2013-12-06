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
		private StubCanvasFactory _stubCanvasFactory;
		private MockRepository _mocks;

		class StubCanvasFactory : IDiffableCanvasFactory
		{
			private int callCount = 0;
			private MockRepository mocks;
			public IDiffableCanvas flushedCavnas;
			public IDiffableCanvas dirtyCanvas;
			public StubCanvasFactory(MockRepository mocks)
			{
				this.mocks = mocks;
				flushedCavnas = mocks.DynamicMock<IDiffableCanvas>();
				dirtyCanvas = mocks.DynamicMock<IDiffableCanvas>();
			}

			public IDiffableCanvas Create()
			{
				// TODO: ISSUE - changing order of cases will break test!
				callCount++;
				switch (callCount)
				{
					case 1:
						return flushedCavnas;
					case 2:
						return dirtyCanvas;
					default:
						return mocks.DynamicMock<IDiffableCanvas>();
				}
			}
		}

		[SetUp]
		public void Setup()
		{
			_mocks = new MockRepository();
			_config = new DefaultConfig();
			_stubCanvasFactory = new StubCanvasFactory(_mocks);
			_diffFlushableCanvas = new DiffFlushableCanvas(_config, _stubCanvasFactory);
		}

		[Test]
		public void FlushChanges_NothingDrawn_WriteToConsoleCalledOnDiff()
		{
			// Setup empty diff canvas stub to be returned by calls to Diff() on flushedCavnas
			IDiffableCanvas mockDiffCanvas = _mocks.DynamicMock<IDiffableCanvas>();
			mockDiffCanvas.Stub(x => x.GetEnumerator()).Return(((IEnumerable<ICanvasChar>)new ICanvasChar[0]).GetEnumerator());
			_stubCanvasFactory.flushedCavnas.Stub(x => x.Diff(null)).Return(mockDiffCanvas).IgnoreArguments();
			_mocks.ReplayAll();
			_diffFlushableCanvas.FlushChanges();
			mockDiffCanvas.AssertWasCalled(x=>x.WriteToConsole());
		}

		[Test]
		public void WriteCurrent_NothingDrawn_WriteToConsoleCalledOnDirty()
		{
			_mocks.ReplayAll();
			_diffFlushableCanvas.WriteCurrent();
			_stubCanvasFactory.dirtyCanvas.AssertWasCalled(x => x.WriteToConsole());
		}

		[Test]
		public void WriteCurrent_NothingDrawn_FlushedCanvasSetToPreviousDirtyCanvas()
		{
			// Setup empty diff canvas stub to be returned by calls to Diff()
			IDiffableCanvas stubDiffCanvas = _mocks.DynamicMock<IDiffableCanvas>();
			_stubCanvasFactory.dirtyCanvas.Stub(x => x.Diff(null)).Return(stubDiffCanvas).IgnoreArguments();
			_diffFlushableCanvas.WriteCurrent(); // Persist dirty canvas and set dirty canvas to blank

			_mocks.ReplayAll();
			_diffFlushableCanvas.FlushChanges(); // Expect: Diff(..) called on previously dirty canvas
			_stubCanvasFactory.dirtyCanvas.AssertWasCalled(x => x.Diff(Arg<IDiffableCanvas>.Is.Anything));
		}

		[Test]
		public void WriteCurrent_NothingDrawn_DirtyCanvasSetToBlankCanvas()
		{
			// Setup empty diff canvas stub to be returned by calls to Diff()
			IDiffableCanvas stubDiffCanvas = _mocks.DynamicMock<IDiffableCanvas>();
			_stubCanvasFactory.dirtyCanvas.Stub(x => x.Diff(null)).Return(stubDiffCanvas).IgnoreArguments();
			_diffFlushableCanvas.WriteCurrent(); // Persist dirty canvas and set dirty canvas to blank

			_mocks.ReplayAll();
			_diffFlushableCanvas.WriteCurrent(); //Expect: WriteToConsole() NOT called on previous dirty canvas
			_stubCanvasFactory.dirtyCanvas.AssertWasNotCalled(x => x.WriteToConsole());
		}
	}
}
