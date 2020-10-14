using System;
using Level.Interfaces;
using UnityEngine;
using Util;

namespace Level.Data {
	[Serializable]
	public class HumbleWall : ILevelEncodable<HumbleWall> {
		public WallType wallType;


		public string Encode2String() {
			return wallType.ToString();
		}

		public HumbleWall FromString(string s) {

			wallType = (WallType) Enum.Parse(typeof(WallType), s);
			return this;
		}

		public T FromString<T>(string s) {
			throw new NotImplementedException();
		}

		public static HumbleWall FromEncoding(string encoding) {
			HumbleWall curr = new HumbleWall();

			curr.wallType = (WallType) Enum.Parse(typeof(WallType), encoding);
			return curr;
		}
	}
}