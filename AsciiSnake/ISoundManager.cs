namespace dk.ChrisGulddahl.AsciiSnake
{
	interface ISoundManager
	{
		/// <summary>
		/// Property for muting the game.
		/// </summary>
		bool Muted { get; set; }
		
		/// <summary>
		/// Asynchronously plays sound to signify that an apple has been eaten.
		/// </summary>
		void PlayEatAppleSound();

		/// <summary>
		/// Asynchronously plays sound to signify that the snake has crashed into something.
		/// </summary>
		void PlayCrashedSound();
	}
}
