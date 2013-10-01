using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace dk.ChrisGulddahl.AsciiSnake
{
	interface IApples : IDrawable
	{
		/**
		* Amount of time an apple is displayed for expressed in game ticks
		*/
		int AppleLifetime { get; set; }

		/**
		 * Minimum number of apples at all times
		 */
		int MinApples { get; set; }

		/**
		 * Remove apples that are too old and
		 * add new apples if there are less than defined in the MinApples property. 
		 * Current game tick should be saved with the apple to later determine its age.
		 */
		void RemoveOldApplesAndAddNewIfNeeded(int currentGameTick);

		/**
		 * Returns whether an apple exists at the given position
		 */
		bool HasAppleAt(Point position);

		void EatAppleAt(Point position);
	}
}
