using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Level.Data;
using Level.MarkovModels;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine;

namespace Level {
	public class PathGenerator : MonoBehaviour {
		[SerializeField]
		private List<SegmentData> markovSegments;

		private StringMarkov trainer; 

		[Button]
		private void TrainMarkov() {
			string allEncodings = markovSegments
				.Select(seg => seg.Encode2String())
				.Aggregate((partialPhrase, segEncoding) => $"{partialPhrase} {segEncoding}");
			
			
			trainer.Learn(allEncodings);
		}

//TODO
//		public IEnumerable NextSegment() {
//		
//			
//		} 
		private void Start() {
			PopulateSegments();
			TrainMarkov();
		}

		public void PopulateSegments() {
			string[] assetNames = AssetDatabase.FindAssets("t: SegmentData", new[] {"Assets/ScriptableObjects"});
			markovSegments.Clear();

			foreach (string SOName in assetNames) {
				var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
				SegmentData seg = AssetDatabase.LoadAssetAtPath<SegmentData>(SOpath);
				markovSegments.Add(seg);
			}
		}
	}
}