using System;
using System.Threading;

namespace dk.ChrisGulddahl.AsciiSnake
{
	class SoundManager : ISoundManager
	{
		public SoundManager()
		{
			Muted = false;
		}

		public bool Muted { get; set; }

		public void PlayEatAppleSound()
		{
			if (Muted)
				return;
			var soundThread = new Thread(new ThreadStart(() =>
			{
				Console.Beep(1000, 50);
				Console.Beep(1400, 75);
			}));
			soundThread.Start();
		}

		public void PlayCrashedSound()
		{
			if (Muted)
				return;
			var soundThread = new Thread(new ThreadStart(() => { 
				Console.Beep(1000, 100);
				Console.Beep(950, 100);
				Console.Beep(900, 100);
				Console.Beep(850, 100);
				Console.Beep(800, 100);
				Console.Beep(750, 100);
				Console.Beep(700, 100);
			}));
			soundThread.Start();
		}
	}
}
