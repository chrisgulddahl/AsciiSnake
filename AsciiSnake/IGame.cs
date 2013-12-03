namespace dk.ChrisGulddahl.AsciiSnake
{
	interface IGame
	{
		/// <summary>
		/// Number of ticks since game started.A tick occurs every time the snake moves.
		/// </summary>
		int CurrentTick { get; }

		/// <summary>
		/// Game score.
		/// </summary>
		int Score { get;  }

		/// <summary>
		/// Start and run the game by repeatedly calling <see cref="Tick"/>.
		/// </summary>
		void Start();

		/// <summary>
		/// Run single step (tick) of the game. I.e. move the snake once, test for crashed etc.
		/// </summary>
		void Tick();
	}
}
