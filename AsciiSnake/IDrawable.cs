namespace dk.ChrisGulddahl.AsciiSnake
{
	/// <summary>
	/// Models objects that are drawable in the game. E.g. snake, border, apples.
	/// </summary>
	public interface IDrawable
	{
		/// <summary>
		/// Draw object to canvas.
		/// </summary>
		/// <param name="canvas">Canvas to draw object on.</param>
		void Draw(ICanvas canvas);
	}
}
