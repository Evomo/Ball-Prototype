using Character;
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
		[SerializeField] private AbstractCollectable _collectable;

		private Transform _collectablePosition;

		public Material standardMaterial, boostMaterial;

		public WallType CurrType {
			get => data.wallType;
			set {
				MaterialFactory();
				data.wallType = value;
			}
		}


		private void MaterialFactory() {
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

		public void InitFromHumble(HumbleWall d, TunnelDirection dir) {
			direction = dir;
			data = d;


			if (d.collectableAsset != null) {
				AbstractCollectable c = PoolManager.SpawnObject(d.collectableAsset.collectable.gameObject,
					_collectablePosition.position,
					Quaternion.identity).GetComponent<AbstractCollectable>();

				c.Init(d.collectableAsset, dir);
				_collectable = c;
			}

			MaterialFactory();
		}


		void Awake() {
			meshRenderer = GetComponent<MeshRenderer>();
			_collectablePosition = transform.GetChild(0);
		}

		public void Recycle() {
			if (_collectable != null) {
				PoolManager.ReleaseObject(_collectable.gameObject);
				_collectable = null;
			}
		}

		public void ConsumeCollectable(Slime slime) {
			if (_collectable != null) {
				_collectable.Collect(slime);
			}
		}
	}
}