using System;
using Character;
using MotionAI.Core.Util;
using UnityEngine;
using UnityEngine.Events;
using Util;
using Util.Pool;

namespace Collectables {
	public abstract class AbstractCollectable : MonoBehaviour {
		[SerializeField] private TunnelDirection position;

		public UnityEvent onCollected;

		public float value;

		public virtual void Init(CollectableAsset hc, TunnelDirection pos) {
			value = hc.collectableValue;
			position = pos;
		}

		protected abstract void ApplyCollectable(Slime slime);


		public void Collect(Slime slime) {
			ApplyCollectable(slime);
			onCollected.Invoke();
			PoolManager.ReleaseObject(gameObject);
		}

		public void Update() {
			transform.Rotate(Vector3.up, 2);
		}
	}
}