using System;
using System.Collections.Generic;
using Collectables;
using Level.Data;
using MotionAI.Core.Util;
using UnityEditor;
using UnityEngine;

namespace Manager {
	public class CollectableFactory : Singleton<CollectableFactory> {
		private Dictionary<int, CollectableAsset> _assets;


		public void Awake() {
			PopulateSegments();
		}

		private void PopulateSegments() {
			_assets = new Dictionary<int, CollectableAsset>();
			foreach (CollectableAsset coll in Resources.FindObjectsOfTypeAll<CollectableAsset>()) {
				_assets[coll.GetHashCode()] = coll;
			}
		}

		public CollectableAsset Get(int hash) {
			CollectableAsset ass;

			_assets.TryGetValue(hash, out ass);

			return ass;
		}
	}
}