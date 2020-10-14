using System.Collections.Generic;
using System.Linq;
using Level.Data;
using Level.MarkovModels;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine;

namespace Level.Components {
	public class PathGenerator : MonoBehaviour {
		[SerializeField] private List<SegmentAsset> markovSegments;

		private StringMarkov trainer;

		private bool _isTrained;

		[Button]
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

		[Button()]
		private HumbleSegment NextSegment() {
			if (!_isTrained) {
				TrainMarkov();
			}
			
	

			string next = trainer.Walk().FirstOrDefault();

			Debug.Log(next);

			return new HumbleSegment();
		}

		private void Start() {
			PopulateSegments();
			TrainMarkov();
		}

		[Button]
		public void PopulateSegments() {
			string[] assetNames = AssetDatabase.FindAssets("t: SegmentAsset", new[] {"Assets/ScriptableObjects"});
			if (markovSegments != null) {
				markovSegments.Clear();
			}
			else {
				markovSegments = new List<SegmentAsset>();
			}

			foreach (string SOName in assetNames) {
				var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
				SegmentAsset seg = AssetDatabase.LoadAssetAtPath<SegmentAsset>(SOpath);
				markovSegments.Add(seg);
			}
		}
	}
}