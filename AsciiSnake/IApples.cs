﻿using System.Drawing;

namespace dk.ChrisGulddahl.AsciiSnake
{
	public interface IApples : IDrawable
	{
		INewAppleLocationStrategy NewAppleLocationStrategy { get; set; }

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

		/**
		 * Remove apple at position
		 */
		void RemoveAppleAt(Point position);
	}
}
