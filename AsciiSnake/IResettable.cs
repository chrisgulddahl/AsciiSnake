using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dk.ChrisGulddahl.AsciiSnake
{
	/// <summary>
	/// Object which can be reset to just-after-instantiation state.
	/// </summary>
	public interface IResettable
	{
		/// <summary>
		/// Reset object to the state it had just after instantiation.
		/// </summary>
		void Reset();
	}
}
