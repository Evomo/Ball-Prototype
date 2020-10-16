using System;
using System.Linq;
using System.Text.RegularExpressions;
using Collectables;
using Level.Interfaces;
using Manager;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Util;

namespace Level.Data {
	[Serializable]
	public class HumbleWall : ILevelEncodable<HumbleWall> {
		public WallType wallType;
		public CollectableAsset collectableAsset;

		public string Encode2String() {
			string hash = collectableAsset != null ? collectableAsset.GetHashCode().ToString() : "null";

			string r = $"{wallType.ToString()}<{hash}";
			return r;
		}


		public HumbleWall InitFromString(string s) {
			string[] tokens = Regex.Split(s, "<");
			wallType = (WallType) Enum.Parse(typeof(WallType), tokens.First());
			int hash;
			if (Int32.TryParse(tokens.Last(), out hash)) {
				collectableAsset = CollectableFactory.Instance.Get(hash);
			}

			return this;
		}
	}
}