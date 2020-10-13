using System;
using System.Collections.Generic;
using Level.Data;
using UnityEngine;
using Util;

namespace Level {
	public class Segment : MonoBehaviour {
		
		[SerializeField] public WallManager Walls;


		public Transform start, end;

		public Segment previous, next;
		
		public void Init(ref Queue<Segment> backTrackSegments) {
			Walls.Init(ref backTrackSegments);
		}

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
	}
}