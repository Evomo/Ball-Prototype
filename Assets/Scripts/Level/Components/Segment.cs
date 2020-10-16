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
		[SerializeField] private WallManager walls;
		private HumbleSegment _humbleSegment;


		public Transform start, end;

		public Segment previous, next;


		public void Init(HumbleSegment seg) {
			foreach (TunnelDirection dir in EnumUtils.GetValues<TunnelDirection>()) {
				GetWall(dir).InitFromHumble(seg.GetWall(dir), dir);
			}
		}

		public void ConnectSegmentTo(Segment s) {
			Transform nextTrans = s.transform;
			Vector3 displacement = nextTrans.position - s.start.position;
			nextTrans.position = end.position + displacement;

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
				RecyclePrevious();


				GetWall(slime.currGravityDirection).ConsumeCollectable(slime);
			}
		}

		public void RecyclePrevious() {
			if (previous != null) {
				LevelGenerator.Instance.RecycleItem(previous);
			}
		}
	}
}