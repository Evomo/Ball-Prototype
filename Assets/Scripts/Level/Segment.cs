using System;
using UnityEngine;
using Util;

namespace Level {
	public class Segment : MonoBehaviour {
		[Serializable]
		public class WallManager {
			public Wall North, South, East, West;

			public Wall GetWall(SpawnPosition pos) {
				switch (pos) {
					case SpawnPosition.North:
						return North;
					case SpawnPosition.East:
						return East;
					case SpawnPosition.South:
						return South;
					case SpawnPosition.West:
						return West;
					default:
						return null;
				}
			}

			public void Init() {
				foreach (SpawnPosition p in Enum.GetValues(typeof(SpawnPosition))) {
					GetWall(p)?.Init();
				}
			}
		}


		public Transform start, end;
		[SerializeField] public WallManager Walls;

		public void Init() {
			Walls.Init();
		}

		public void ConnectSegmentTo(Segment s) {
			Transform segmentTrans = s.transform;
			Vector3 displacement = segmentTrans.position - s.start.position;
			segmentTrans.position = end.position + displacement;
			Init();
		}

		public static Segment Spawn(GameObject segmentPrefab, Segment lastSegment) {
			Segment s = SimplePool.Spawn(segmentPrefab, Vector3.zero, Quaternion.identity)
				.GetComponent<Segment>();

			if (lastSegment != null) {
				lastSegment.ConnectSegmentTo(s);
			}

			return s;
		}
	}
}