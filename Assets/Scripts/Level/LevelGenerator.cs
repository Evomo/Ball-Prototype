using System;
using System.Collections.Generic;
using Level.Data;
using NaughtyAttributes;
using UnityEngine;

namespace Level {
	[RequireComponent(typeof(PathGenerator))]
	public class LevelGenerator : MonoBehaviour {
		public GameObject segmentPrefab;
		public Segment currentSegment;

		public PathGenerator path; 

		[SerializeField]
		private Queue<Segment> _backTrackSegments;
		[Range(1, 5)] public int backTrackCount = 1;

		private void Start() {
			path = GetComponent<PathGenerator>();
			_backTrackSegments = new Queue<Segment>();
		}

		[Button]
		public void NextSegment() {
			Segment s = Segment.Spawn(segmentPrefab, currentSegment);

			if (_backTrackSegments.Count == backTrackCount) {
				_backTrackSegments.Dequeue();
			}

			_backTrackSegments.Enqueue(s);

			s.Init(ref _backTrackSegments);
			currentSegment = s;
		}
	}
}