using Level;
using Level.Components;
using UnityEngine;
using Util;
using Vector3 = UnityEngine.Vector3;

namespace Character {
	public class Slime : TunnelErrorChecker {
		public CharacterController controller;
		private Segment _currentSegment;

		[Range(0, 1)] public float minStickDistance;
		[Range(1, 20)] public float speed;
		private Vector3 _moveVector;


		private Vector3 Direction =>
			Vector3.right; //(_currentSegment.next.transform.position - transform.position).normalized;


		public Segment CurrentSegment {
			get => _currentSegment;
			set {
				_currentSegment = value;
				_currentSegment.RecyclePrevious();
			}
		}

		public bool IsGrounded {
			get {
				RaycastHit hit;
				if (Physics.Raycast(transform.position, GravityVector, out hit, Mathf.Infinity)) {
					if (hit.distance < minStickDistance) {
						return true;
					}
				}

				return false;
			}
		}


		public void Start() {
			controller = GetComponent<CharacterController>();
		}


		private void Update() {
			MovementCorrection();
			ShowDebugVectors();

			controller.Move(Direction * (Time.deltaTime * speed));
		}


		private void MovementCorrection() {
			_moveVector = Vector3.zero;
			if (!IsGrounded) {
				_moveVector += GravityVector ;
			}

			if (DriftPassesThreshold) {
				_moveVector += ErrorDrift * (gravMultiplier);
			}

			Debug.DrawLine(transform.position, transform.position + ErrorDrift, Color.magenta);
			Debug.DrawLine(transform.position, transform.position + GravityVector * 10, Color.green);
			controller.Move(_moveVector.normalized * (speed * Time.deltaTime));
		}
	}
}