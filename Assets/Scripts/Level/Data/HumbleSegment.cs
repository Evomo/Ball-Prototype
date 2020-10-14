using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Level.Components;
using Level.Interfaces;
using UnityEngine;
using Util;

namespace Level.Data {
	[Serializable]
	public class HumbleSegment : ILevelEncodable<HumbleSegment> {
		public HumbleWall North, South, East, West;


		private Dictionary<TunnelDirection, int> _previousPathWeights;

		public static HumbleSegment CreateFromString(string s) {
			return new HumbleSegment().FromString(s);
		}

		public HumbleSegment() {
			North = new HumbleWall();
			South = new HumbleWall();
			East = new HumbleWall();
			West = new HumbleWall();
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


		public string Encode2String() {
			return $"{North.Encode2String()}-{South.Encode2String()}-{East.Encode2String()}-{West.Encode2String()}";
		}


		public HumbleSegment FromString(string s) {

			string[] splits = Regex.Split(s, "-");
			for (int i = 0; i < splits.Length; i++) {
				GetWall((TunnelDirection) i).FromString(splits[i]);
			}

			return this;
		}

	}
}