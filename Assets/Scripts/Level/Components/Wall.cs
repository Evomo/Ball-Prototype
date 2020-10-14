using Level.Data;
using UnityEngine;
using Util;

namespace Level.Components {
	public class Wall : MonoBehaviour {
		[SerializeField] private MeshRenderer _meshRenderer;
		[SerializeField] private WallType currType;


		[SerializeField] private HumbleWall _data;

		public WallType CurrType {
			get => _data.wallType;
			set {
				MaterialFactory();
				_data.wallType = value;
			}
		}

		public Material standardMaterial, boostMaterial;


		public void MaterialFactory() {
			if (_meshRenderer == null) return;
			switch (CurrType) {
				case WallType.BOOST:
					_meshRenderer.material = boostMaterial;

					break;
				default:
					_meshRenderer.material = standardMaterial;
					break;
			}
		}

		public void Init(HumbleWall data) {
			this._data = data;
			MaterialFactory();
		}


		void Awake() {
			_meshRenderer = GetComponent<MeshRenderer>();
		}
	}
}