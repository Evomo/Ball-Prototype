using System;
using System.Collections.Generic;
using System.Linq;
using Level.Data;
using MotionAI.Core.Util;
using NaughtyAttributes;
using UnityEngine;
using Util;
using Util.Pool;

namespace Level.Components {
	[RequireComponent(typeof(MarkovGenerator))]
	public class LevelGenerator : Singleton<LevelGenerator> {
		public Segment segmentComponent;


		[SerializeField] private Segment currentSegment;

		[SerializeField, Range(4, 20)] private int recycleThreshold;
		public MarkovGenerator markov;
		private Queue<Segment> _toRecycle;

		private Queue<HumbleSegment> _segmentQueue;

		private void Start() {
			_toRecycle = new Queue<Segment>();
			markov = GetComponent<MarkovGenerator>();

			int warmSize = 50;


			PoolManager.WarmPool(segmentComponent.gameObject, warmSize);
			for (int i = 0; i < warmSize; i++) {
				BuildNextSegmentPhrase();
			}
		}

		public void RecycleItem(Segment s) {
			_toRecycle.Enqueue(s);


			BuildNextSegmentPhrase();
			while (_toRecycle.Count > recycleThreshold) {
				Segment recycled = _toRecycle.Dequeue();
				PoolManager.ReleaseObject(recycled.gameObject);
			}
		}

		private void SpawnFromHumble(HumbleSegment nextHumble) {
			if (PoolManager.AmountActive(segmentComponent.gameObject, false) > 1) {
				Segment s = PoolManager.SpawnObject(segmentComponent.gameObject).GetComponent<Segment>();
				if (currentSegment != null) {
					currentSegment.ConnectSegmentTo(s);
				}


				s.Init(nextHumble);
				currentSegment = s;
			}
		}

		private void BuildNextSegmentPhrase() {
			if (_segmentQueue == null) {
				_segmentQueue = new Queue<HumbleSegment>();
			}

			if (_segmentQueue.Count == 0) {
				foreach (HumbleSegment segment in markov.NextSegmentGenerator()) {
					_segmentQueue.Enqueue(segment);
				}
			}

			HumbleSegment s = _segmentQueue.Dequeue();

			SpawnFromHumble(s);
		}
	}
}