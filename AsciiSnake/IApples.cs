using System.Drawing;

namespace dk.ChrisGulddahl.AsciiSnake
{
	public interface IApples : IDrawable, IResettable
	{
		INewAppleLocationStrategy NewAppleLocationStrategy { get; set; }

		/// <summary>
		/// Remove apples that are too old and add new apples if there is currently less than
		///  configured in the MinApples property of the injected <see cref="IConfig"/> instance. 
		/// </summary>
		/// <remarks>Current game tick is saved with each new apple to later determine its age.</remarks>
		/// <param name="currentGameTick">Current tick of the game.</param>
		void RefreshApples(int currentGameTick);

		/// <summary>
		/// Returns whether an apple exists at the given position.
		/// </summary>
		/// <param name="position">Position to test for.</param>
		/// <returns>Returns true if an apple exists at the given position. False otherwise.</returns>
		bool HasAppleAt(Point position);

		/// <summary>
		/// Remove apple at position. If no apple exists at position nothing will happen.
		/// </summary>
		/// <param name="position">Position on which to remove apple.</param>
		void RemoveAppleAt(Point position);
	}
}
