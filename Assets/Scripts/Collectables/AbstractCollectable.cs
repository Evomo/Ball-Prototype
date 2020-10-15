using System;
using Character;
using UnityEngine;
using Util.Pool;

namespace Collectables {
	public abstract class AbstractCollectable : MonoBehaviour {
		public abstract void ApplyCollectable();


		public float value;

		public void Init(CollectableAsset hc) {
			value = hc.collectableValue;
		}

		public void Update() {
			transform.Rotate(Vector3.up, 2);
		}


	}
}