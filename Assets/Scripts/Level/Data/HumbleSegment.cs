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

		public static string matchPattern = @"([^\)|^\(]+)";

		private Regex _regx = new Regex(matchPattern);
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

		public string Encode2String() {
			return
				$"({North.Encode2String()})({South.Encode2String()})({East.Encode2String()})({West.Encode2String()})";
		}


		public HumbleSegment FromString(string s) {
			MatchCollection splits = _regx.Matches(s);
			int wallIt = 0;
			foreach (Match split in splits) {
				GetWall((TunnelDirection) wallIt).FromString(split.Value);
				wallIt++;
			}

			return this;
		}
	}
}