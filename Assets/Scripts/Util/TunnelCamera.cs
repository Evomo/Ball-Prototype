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

		private Vector3 moveVec;

		void Start() {
			_cam = GetComponent<Camera>();
			slime = FindObjectOfType<Slime>();
			_startDiff = slime.transform.position - transform.position;
		}


		void Update() {
			ShowDebugVectors();
			currGravityDirection = EnumUtils.Perpendicular(slime.currGravityDirection);
			moveVec = slime.transform.position - _startDiff;
			if (DriftPassesThreshold) {
				moveVec += ErrorDrift;
			}

			transform.position = Vector3.Lerp(transform.position, moveVec, gravMultiplier * Time.deltaTime);

			
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