using System;
using System.Collections.Generic;
using Character;
using Level.Data;
using UnityEngine;
using Util;
using Util.Pool;

namespace Level.Components {
	[Serializable]
	public class WallManager {
		public Wall North, East, South, West;
	}

	public class Segment : MonoBehaviour {
		private HumbleSegment _humbleSegment;


		public Transform start, end;

		public Segment previous, next;

		[SerializeField] public WallManager walls;


		public void Init(HumbleSegment seg) {
			walls.North.Init(seg.GetWall(TunnelDirection.NORTH), TunnelDirection.NORTH);
			walls.South.Init(seg.GetWall(TunnelDirection.SOUTH), TunnelDirection.SOUTH);
			walls.East.Init(seg.GetWall(TunnelDirection.EAST), TunnelDirection.EAST);
			walls.West.Init(seg.GetWall(TunnelDirection.WEST), TunnelDirection.WEST);
		}

		public void ConnectSegmentTo(Segment s) {
			Transform segmentTrans = s.transform;
			Vector3 displacement = segmentTrans.position - s.start.position;
			segmentTrans.position = end.position + displacement;

			next = s;
			s.previous = this;
		}


		public Wall GetWall(TunnelDirection pos) {
			switch (pos) {
				case TunnelDirection.NORTH:
					return walls.North;
				case TunnelDirection.EAST:
					return walls.East;
				case TunnelDirection.SOUTH:
					return walls.South;
				case TunnelDirection.WEST:
					return walls.West;
				default:
					return null;
			}
		}

		private void OnTriggerEnter(Collider other) {
			Slime slime = other.gameObject.GetComponent<Slime>();
			if (slime != null) {
				slime.CurrentSegment = this;
				GetWall(slime.currGravityDirection).RedeemCollectable();

				RecyclePrevious();
			}
		}

		public void RecyclePrevious() {
			if (previous != null) {
				LevelGenerator.Instance.RecycleItem(previous);
			}
		}
	}
}