using System;
using System.Collections.Generic;
using Level.Components;
using Level.Interfaces;
using Util;

namespace Level.Data {
	[Serializable]
	public class HumbleSegment : ILevelEncodable {
		public HumbleWall North, South, East, West;


		private Dictionary<TunnelDirection, int> _previousPathWeights;

		public HumbleSegment() {
			_previousPathWeights = new Dictionary<TunnelDirection, int>();
		}

		public HumbleWall GetWall(TunnelDirection pos) {
			switch (pos) {
				case TunnelDirection.NORTH:
					return North;
				case TunnelDirection.EAST:
					return East;
				case TunnelDirection.SOUTH:
					return South;
				case TunnelDirection.WEST:
					return West;
				default:
					return null;
			}
		}

		public List<HumbleWall> GetAllWalls() {
			List<HumbleWall> wallList = new List<HumbleWall>();
			foreach (TunnelDirection p in Enum.GetValues(typeof(TunnelDirection))) {
				HumbleWall currHumbleWall = GetWall(p);
				if (currHumbleWall != null) {
					wallList.Add(currHumbleWall);
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

		private void AddToWeights(ref Dictionary<TunnelDirection, int> previousPathWeights) {
			foreach (TunnelDirection p in Enum.GetValues(typeof(TunnelDirection))) {
				HumbleWall currHumbleWall = GetWall(p);

				if (currHumbleWall != null && currHumbleWall.wallType == WallType.BOOST) {
					int amount;
					previousPathWeights.TryGetValue(p, out amount);
					previousPathWeights[p] = amount + 1;
				}
			}
		}

		public string Encode2String() {
			return $"{North.Encode2String()}-{South.Encode2String()}-{East.Encode2String()}-{West.Encode2String()}";
		}
	}
}