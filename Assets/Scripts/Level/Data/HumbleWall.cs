using System;
using Level.Interfaces;
using Util;

namespace Level.Data {
	[Serializable]
	public class HumbleWall : ILevelEncodable {
		public WallType wallType;


		public string Encode2String() {
			return wallType.ToString();
		}

		public static HumbleWall FromEncoding(string encoding) {
			HumbleWall curr = new HumbleWall();

			curr.wallType = (WallType) Enum.Parse(typeof(WallType), encoding);
			return curr;
		}
	}
}