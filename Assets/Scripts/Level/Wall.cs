using Level.Data;
using UnityEngine;

namespace Level {
	public class Wall : MonoBehaviour {
		[SerializeField] private MeshRenderer _meshRenderer;
		[SerializeField] private WallType currType;


		private WallData data;

		public WallType CurrType {
			get => data.wallType;
			set {
				MaterialFactory();
				data.wallType = value;
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
			this.data = data;
			MaterialFactory();
		}



		void Awake() {
			_meshRenderer = GetComponent<MeshRenderer>();


			//TODO DEBUG
			CurrType = Random.Range(0, 10) > 5 ? WallType.NORMAL : WallType.BOOST;
		}
	}
}