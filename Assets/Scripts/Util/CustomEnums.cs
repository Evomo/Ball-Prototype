using System;

namespace Util {
	public enum TunnelDirection {
		NORTH,
		SOUTH,
		EAST,
		WEST
	}

	public enum WallType {
		NORMAL,
		BOOST
	}

	public enum CollectableType {
		COIN,
		TIME
	}

	public static class EnumUtils {
		public static TunnelDirection Opposite(TunnelDirection d) {
			switch (d) {
				case TunnelDirection.NORTH:
					return TunnelDirection.SOUTH;
				case TunnelDirection.SOUTH:
					return TunnelDirection.NORTH;
				case TunnelDirection.EAST:
					return TunnelDirection.WEST;
				case TunnelDirection.WEST:

					return TunnelDirection.EAST;
				default:
					throw new ArgumentOutOfRangeException(nameof(d), d, null);
			}
		}
	}
}