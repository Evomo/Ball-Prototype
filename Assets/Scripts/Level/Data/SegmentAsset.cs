using System;
using System.Collections.Generic;
using Level.Interfaces;
using NaughtyAttributes;
using UnityEngine;

namespace Level.Data {
	[CreateAssetMenu(menuName = "Slime/Segment Data")]
	public class SegmentAsset : ScriptableObject, ILevelEncodable {
		public List<HumbleSegment> humbleSegments;


		public string Encode2String() {
			List<string> segments = new List<string>();
			foreach (HumbleSegment currSegment in humbleSegments) {
				segments.Add(currSegment.Encode2String());
			}

			return String.Join(" ", segments);
		}
	}
}