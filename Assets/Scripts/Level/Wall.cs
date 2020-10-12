using UnityEngine;

namespace Level {
	public class Wall : MonoBehaviour {
		private MeshRenderer _meshRenderer;

		[SerializeField] private bool hasBoost;

		public bool HasBoost {
			get => hasBoost;
			set {
				SetMaterial();

				hasBoost = value;
			}
		}

		public Material standardMaterial, boostMaterial;


		public void SetMaterial() {
			Debug.Log("hjashjkkjasd");
			if (HasBoost) {
				_meshRenderer.material = boostMaterial;
			}
			else {
				_meshRenderer.material = standardMaterial;
			}
		}

		public void Init() {
			hasBoost = Random.Range(0, 10) > 5;
			SetMaterial();
		}

		void Start() {
			_meshRenderer = GetComponent<MeshRenderer>();
		}
	}
}