using System;
using System.Collections.Generic;
using UnityEngine;

namespace Level.Data {
	public enum WallType {
		NORMAL,
		BOOST
	}

	[Serializable]
	public class WallManager {
		public void Init(ref Queue<Segment> backTrackSegments) {
		
			Debug.Log("init");
			// throw new System.NotImplementedException();
		}
	}
	
	[Serializable]
	public class WallData {
		public WallType wallType;
	}
}