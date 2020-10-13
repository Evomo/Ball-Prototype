using Level.Data;
using UnityEngine;

namespace Level {
	public class Wall : MonoBehaviour {
		[SerializeField] private MeshRenderer _meshRenderer;
		[SerializeField] private WallType currType;


		[SerializeField]
		private WallData _data;

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

		public void Init(WallData data) {
			this._data = data;
			MaterialFactory();
		}



		void Awake() {
			_meshRenderer = GetComponent<MeshRenderer>();


			//TODO DEBUG
			CurrType = Random.Range(0, 10) > 5 ? WallType.NORMAL : WallType.BOOST;
		}
	}
}