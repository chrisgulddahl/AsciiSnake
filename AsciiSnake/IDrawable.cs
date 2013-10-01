

namespace dk.ChrisGulddahl.AsciiSnake
{
	/**
	 * Models objects that are drawable in the game. E.g. snake, border, apples.
	 */
	interface IDrawable
	{
		/**
		 * Reference to an instance of a console wrapper.
		 */
		IConsoleWrapper Console { get; }

		/**
		 * Draw object to console .
		 */
		void Draw();

		/**
		 * Update drawing in console to match current state.
		 */
		void Redraw();

		/**
		 * Returns whether the object has changed in such a way that is need to be redrawn.
		 */
		bool NeedsRedraw();
	}
}
