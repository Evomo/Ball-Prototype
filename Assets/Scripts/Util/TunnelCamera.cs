using Character;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Util {
	public class TunnelCamera : MonoBehaviour {
		[SerializeField] private Slime slime;

		private float _oldFieldOfView;
		private Vector3 _oldPosition;
		private Camera _cam;

		private Vector3 _startDiff;

		private Vector3 moveVec;

		[MinMaxSlider(30, 90), SerializeField] private Vector2 _fov;

		[Range(1, 5)] public float fovChangeSpeed = 3;

		[Range(1, 5)] public float lerpScale = 3;
		[Range(1, 3)] public float cameraDisplacement = 3;
		
		private Vector3 Midpoint => slime.transform.position - _startDiff;
		private TunnelDirection CurrGravityDirection => EnumUtils.Opposite(slime.currGravityDirection);
		private Vector3 GravityVector => EnumUtils.Vector(CurrGravityDirection) * cameraDisplacement;

		void Start() {
			_cam = GetComponent<Camera>();
			slime = FindObjectOfType<Slime>();
			_startDiff = slime.transform.position - transform.position;
		}

		void Update() {
			moveVec = Midpoint + GravityVector;

			// transform.LookAt(slime.transform);
			transform.position = Vector3.Lerp(transform.position, moveVec,
				lerpScale * Time.deltaTime);


			if (slime.IsGrounded) {
				_cam.fieldOfView = Mathf.Lerp(_cam.fieldOfView, _fov.x, fovChangeSpeed * Time.deltaTime);
			}
			else {
				_cam.fieldOfView = Mathf.Lerp(_cam.fieldOfView, _fov.y, fovChangeSpeed * Time.deltaTime);
			}
		}
	}
}