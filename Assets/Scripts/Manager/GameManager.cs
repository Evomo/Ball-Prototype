using System;
using MotionAI.Core.Util;

namespace Manager {
	public class GameManager : Singleton<GameManager> {

		public float timeRemaining;
		public int totalCoins;


		public void StartSession() {

			timeRemaining = 120;
			
			throw new NotImplementedException();
		}

		public void EndSession() {
			
		}
	}
}