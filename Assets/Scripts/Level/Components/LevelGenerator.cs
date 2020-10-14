using System.Collections.Generic;
using Level.Data;
using MotionAI.Core.Util;
using NaughtyAttributes;
using UnityEngine;

namespace Level.Components {
	[RequireComponent(typeof(MarkovGenerator))]
	public class LevelGenerator : Singleton<LevelGenerator> {
		public GameObject segmentPrefab;
		public Segment currentSegment;

		public MarkovGenerator markov;


		private void Awake() {
			markov = GetComponent<MarkovGenerator>();
		}


		[Button()]
		private void BuildNextSegmentPhrase() {
			IEnumerable<HumbleSegment> humbleSegments = markov.NextSegmentGenerator();

			foreach (HumbleSegment nextSegment in humbleSegments) {
				Segment s = Segment.Spawn(segmentPrefab, currentSegment);


				if (currentSegment != null) {
					currentSegment.ConnectSegmentTo(s);
				}

				currentSegment = s;
			}
		}
	}
}