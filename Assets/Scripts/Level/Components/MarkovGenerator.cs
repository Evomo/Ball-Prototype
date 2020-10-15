using System.Collections.Generic;
using System.Linq;
using Level.Data;
using Level.MarkovModels;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine;

namespace Level.Components {
	public class MarkovGenerator : MonoBehaviour {
		[SerializeField] private List<SegmentAsset> markovSegments;

		private Segment lastSegment;
		private StringMarkov trainer;

		private bool _isTrained;

		private void Start() {
			PopulateSegments();
			TrainMarkov();
		}

		private void TrainMarkov() {
			if (trainer == null) {
				trainer = new StringMarkov();
			}

			List<SegmentAsset> segmentAssets = markovSegments
				.Where(seg => seg.humbleSegments?.Count > 0).ToList();

			if (segmentAssets.Count > 0) {
				string allEncodings = segmentAssets.Select(seg => seg.Encode2String())
					.Aggregate((partialPhrase, segEncoding) => $"{partialPhrase} {segEncoding}");
				trainer.Learn(allEncodings);
				_isTrained = true;
			}
			else {
				Debug.LogError("Segment Assets cannot be empty!");
			}
		}

		public IEnumerable<HumbleSegment> NextSegmentGenerator() {
			if (!_isTrained) {
				TrainMarkov();
			}


			string next = trainer.Walk().FirstOrDefault();
			// return HumbleSegment.CreateFromString(next);

			IEnumerable<HumbleSegment> segmentsToSpawn = next?
				.Split()
				.Where(s => !string.IsNullOrEmpty(s))
				.Select(s => HumbleSegment.CreateFromString(s));


			return segmentsToSpawn;
		}


		private void PopulateSegments() {
			if (markovSegments != null) {
				markovSegments.Clear();
			}
			else {
				markovSegments = new List<SegmentAsset>();
			}


			foreach (SegmentAsset seg in Resources.FindObjectsOfTypeAll<SegmentAsset>()) {
				markovSegments.Add(seg);
			}
		}
	}
}