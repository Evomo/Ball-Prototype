using System.Collections.Generic;
using Character;
using Level.Data;
using UnityEngine;
using Util;

namespace Level.Components {
	public class Segment : MonoBehaviour {
		[SerializeField] public HumbleSegment Walls;


		public Transform start, end;

		public Segment previous, next;



		public void ConnectSegmentTo(Segment s) {
			Transform segmentTrans = s.transform;
			Vector3 displacement = segmentTrans.position - s.start.position;
			segmentTrans.position = end.position + displacement;

			next = s;
			s.previous = this;
		}

		public static Segment Spawn(GameObject segmentPrefab, Segment lastSegment) {
			Segment s = SimplePool.Spawn(segmentPrefab, Vector3.zero, Quaternion.identity)
				.GetComponent<Segment>();

			if (lastSegment != null) {
				lastSegment.ConnectSegmentTo(s);
			}

			return s;
		}

		private void OnTriggerEnter(Collider other) {
			Slime slime = other.gameObject.GetComponent<Slime>();
			if (slime != null) {
				slime.CurrentSegment = this;
			}
		}


		public void RecyclePrevious() {
			// if (previous != null) {
			// 	int depth = 0;
			//
			// 	Segment curr = previous;
			// 	while (curr.previous != null) {
			// 		curr = curr.previous;
			// 		depth++;
			// 	}
			//
			// 	if (depth >= 3) {
			// 		SimplePool.Despawn(curr.gameObject);
			// 		LevelGenerator.Instance.NextSegment();
			// 	}
			// }
		}
	}
}