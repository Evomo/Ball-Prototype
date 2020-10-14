using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace Level.Data {
	[CreateAssetMenu(menuName = "Slime/Segment Data")]
	public class SegmentData : ScriptableObject {
		public List<WallManager> walls;


		// Encodes string into a phrase with ids for the type of wall encoded as north-south-east-west
		public string Encode2String() {
			List<string> segmentEncoding = new List<string>();
			foreach (WallManager currManager in walls) {
				List<string> encodings = new List<string>();
				foreach (WallData currWall in currManager.GetAllWalls()) {
					encodings.Add(currWall.ToString());
				}

				segmentEncoding.Add(string.Join("-", encodings));
			}

			return string.Join(" ", segmentEncoding);
		}


		[Button]
		public void DebugTest() {
			Debug.Log(Encode2String());
		}
	}
}