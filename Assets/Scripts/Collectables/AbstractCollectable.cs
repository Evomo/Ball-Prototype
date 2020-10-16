using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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

		private MeshRenderer _meshRenderer;
		public virtual void Init(CollectableAsset hc, TunnelDirection pos) {
			value = hc.collectableValue;
			position = pos;
		}

		protected abstract void ApplyCollectable(Slime slime);


		public void Awake() {
			_meshRenderer = transform.GetComponentsInChildren<MeshRenderer>().First();
		}

		public void Collect(Slime slime) {
			ApplyCollectable(slime);
			onCollected.Invoke();
			StartCoroutine(Recycle());
		}


		private IEnumerator Recycle() {
			_meshRenderer.enabled = false;
			yield return new WaitForSeconds(2);
			_meshRenderer.enabled = true;
			PoolManager.ReleaseObject(gameObject);
		}

		public void Update() {
			transform.Rotate(Vector3.up, 2);
		}
	}
}