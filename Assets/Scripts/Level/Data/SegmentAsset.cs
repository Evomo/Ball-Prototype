using System;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;

namespace Level.Data {
	[CreateAssetMenu(menuName = "Slime/Segment Data")]
	public class SegmentAsset : ScriptableObject {
		public List<HumbleSegment> humbleSegments;


		[Button()]
		private void TestEncoding() {
			Debug.Log(Encode2String());
		}

		[Button()]
		private void TestDecoding() {
			HumbleSegment seg = humbleSegments.First();
			seg.InitFromString(seg.Encode2String());
		}
		public string Encode2String() {
			List<string> segments = new List<string>();
			foreach (HumbleSegment currSegment in humbleSegments) {
				segments.Add(currSegment.Encode2String());
			}

			return String.Join(" ", segments);
		}
	}
}