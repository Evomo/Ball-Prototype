using System;
using System.Collections.Generic;
using Level.Data;
using MotionAI.Core.Util;
using NaughtyAttributes;
using UnityEngine;

namespace Level {
	[RequireComponent(typeof(PathGenerator))]
	public class LevelGenerator : Singleton<LevelGenerator> {
		public GameObject segmentPrefab;
		public Segment currentSegment;

		public PathGenerator path;

		[SerializeField] private Queue<Segment> _backTrackSegments;
		[Range(1, 5)] public int backTrackCount = 1;

		private void Awake() {
			path = GetComponent<PathGenerator>();
			_backTrackSegments = new Queue<Segment>();


			for (int i = 0; i < 100; i++) {
				NextSegment();
			}
		}

		[Button]
		public void NextSegment() {
			Segment s = Segment.Spawn(segmentPrefab, currentSegment);

			if (_backTrackSegments.Count == backTrackCount) {
				_backTrackSegments.Dequeue();
			}

			_backTrackSegments.Enqueue(s);

			if (currentSegment != null) {
				currentSegment.ConnectSegmentTo(s);
			}

			// s.Init(ref _backTrackSegments);
			currentSegment = s;
		}
	}
}