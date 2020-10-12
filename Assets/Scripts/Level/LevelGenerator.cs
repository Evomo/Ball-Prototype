using System;
using NaughtyAttributes;
using UnityEngine;
using Util;

namespace Level {
	public enum SpawnPosition {
		None,
		North,
		South,
		East,
		West
	}

	public class LevelGenerator : MonoBehaviour {


		public GameObject segmentPrefab;
		public Segment currentSegment;


		[Button]
		public void SpawnSegment() {
			
			Segment s = Segment.Spawn(segmentPrefab,currentSegment);

			currentSegment = s;
		}
	}
}