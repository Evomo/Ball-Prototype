using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
		public static IEnumerable<T> GetValues<T>() {
			return Enum.GetValues(typeof(T)).Cast<T>();
		}

		public static Vector3 Vector(TunnelDirection d) {
			switch (d) {
				case TunnelDirection.NORTH:
					return Vector3.up;
				case TunnelDirection.SOUTH:
					return Vector3.down;
				case TunnelDirection.EAST:
					return Vector3.back;
				case TunnelDirection.WEST:
					return Vector3.forward;
				default:
					return Vector3.up;
			}
		}

		public static TunnelDirection Perpendicular(TunnelDirection d, bool ccw = true) {
			d = ccw ? d : Opposite(d);
			switch (d) {
				case TunnelDirection.NORTH:
					return TunnelDirection.EAST;
				case TunnelDirection.SOUTH:
					return TunnelDirection.WEST;
				case TunnelDirection.EAST:
					return TunnelDirection.SOUTH;
				case TunnelDirection.WEST:
					return TunnelDirection.NORTH;
				default:
					throw new ArgumentOutOfRangeException(nameof(d), d, null);
			}
		}

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