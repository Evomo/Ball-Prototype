using Collectables;
using Level.Data;
using UnityEngine;
using Util;
using Util.Pool;

namespace Level.Components {
	public class Wall : MonoBehaviour {
		[SerializeField] private MeshRenderer meshRenderer;
		[SerializeField] private WallType currType;
		[SerializeField] private TunnelDirection direction;

		[SerializeField] private HumbleWall data;

		private Transform collectablePosition;


		private AbstractCollectable _collectable;

		public WallType CurrType {
			get => data.wallType;
			set {
				MaterialFactory();
				data.wallType = value;
			}
		}

		public Material standardMaterial, boostMaterial;


		public void MaterialFactory() {
			if (meshRenderer == null) return;
			switch (CurrType) {
				case WallType.BOOST:
					meshRenderer.material = boostMaterial;

					break;
				default:
					meshRenderer.material = standardMaterial;
					break;
			}
		}

		public void Init(HumbleWall data, TunnelDirection direction) {
			this.direction = direction;
			this.data = data;

			if (data.collectableAsset != null) {
				AbstractCollectable c = PoolManager.SpawnObject(data.collectableAsset.collectable.gameObject,
					collectablePosition.position,
					Quaternion.identity).GetComponent<AbstractCollectable>();

				c.Init(data.collectableAsset);
			}

			MaterialFactory();
		}


		public void RedeemCollectable() {
			if (_collectable != null) {
				_collectable.ApplyCollectable();
			}
		}

		void Awake() {
			meshRenderer = GetComponent<MeshRenderer>();
			collectablePosition = transform.GetChild(0);
		}
	}
}