using System;
using System.Collections.Generic;
using System.Linq;
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

		[Serializable]
		public class WallData {
			public WallType wallType;


			public override string ToString() {
				return wallType.ToString();
			}

			public static WallData FromEncoding(string encoding) {
				WallData curr = new WallData();

				curr.wallType = (WallType) Enum.Parse(typeof(WallType), encoding);
				return curr;
			}
		}

		[Serializable]
		public class WallManager {
			public WallData North, South, East, West;


			private Dictionary<SpawnPosition, int> _previousPathWeights;

			public WallManager() {
				_previousPathWeights = new Dictionary<SpawnPosition, int>();
			}

			public WallData GetWall(SpawnPosition pos) {
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

			public List<WallData> GetAllWalls() {
				List<WallData> wallList = new List<WallData>();
				foreach (SpawnPosition p in Enum.GetValues(typeof(SpawnPosition))) {
					WallData currWallData = GetWall(p);
					if (currWallData != null) {
						wallList.Add(currWallData);
					}
				}

				return wallList;
			}

			public void Init(ref Queue<Segment> backTrackSegments) {
				_previousPathWeights.Clear();
				foreach (Segment s in backTrackSegments) {
					s.Walls.AddToWeights(ref _previousPathWeights);
				}
			}

			private void AddToWeights(ref Dictionary<SpawnPosition, int> previousPathWeights) {
				foreach (SpawnPosition p in Enum.GetValues(typeof(SpawnPosition))) {
					WallData currWallData = GetWall(p);

					if (currWallData != null && currWallData.wallType == WallType.BOOST) {
						int amount;
						previousPathWeights.TryGetValue(p, out amount);
						previousPathWeights[p] = amount + 1;
					}
				}
			}
		}