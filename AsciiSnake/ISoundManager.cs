using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dk.ChrisGulddahl.AsciiSnake
{
	interface ISoundManager
	{
		/**
		 * Property for muting the game
		 */
		bool Muted { get; set; }
		/**
		 * Plays a sound. Should be asynchronous.
		 */
		void PlayEatAppleSound();

		/**
		 * Plays a sound. Should be asynchronous.
		 */
		void PlayCrashedSound();
	}
}
