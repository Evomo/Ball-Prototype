using Character;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Util {
	public class TunnelCamera : TunnelErrorChecker {
		[SerializeField] private Slime slime;

		private float _oldFieldOfView;
		private Vector3 _oldPosition;
		private Camera _cam;

		private Vector3 _startDiff;

		void Start() {
			_cam = GetComponent<Camera>();
			slime = FindObjectOfType<Slime>();
			_startDiff = slime.transform.position - transform.position;
		}


		void Update() {
			currGravityDirection = EnumUtils.Opposite(slime.currGravityDirection);
			transform.position = slime.transform.position - _startDiff;
			// if (DriftPassesThreshold) {
				// transform.position -= ErrorDrift;
			// }

			if (slime.IsGrounded) {
				_cam.fieldOfView = 90 + (_oldFieldOfView - 90) * 0.9f;
				_oldFieldOfView = _cam.fieldOfView;
			}
			else {
				_cam.fieldOfView = 95 + (_oldFieldOfView - 95) * 0.9f;
				_oldFieldOfView = _cam.fieldOfView;
			}
		}
	}
}