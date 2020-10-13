using Level;
using UnityEngine;
using Util;
using Vector3 = UnityEngine.Vector3;

namespace Character {
	public class Slime : MonoBehaviour {
		public CharacterController controller;
		private Segment _currentSegment;

		[Range(0, 1)] public float minStickDistance;
		[Range(0, 1)] public float driftMargin;
		[Range(1, 20)] public float speed;
		[Range(1, 20)] public float gravMultiplier;
		private Vector3 _moveVector;

		private Vector3 errorDrift;

		private Vector3 Direction =>
			Vector3.right; //(_currentSegment.next.transform.position - transform.position).normalized;

		public TunnelDirections currGravityDirection;
		private RaycastHit _rightHit, _leftHit;

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
				// Does the ray intersect any objects excluding the player layer
				if (Physics.Raycast(transform.position, GravityVector(), out hit, Mathf.Infinity)) {
					if (hit.distance < minStickDistance) {
						return true;
					}
				}

				return false;
			}
		}

		private Vector3 GravityVector() {
			switch (currGravityDirection) {
				case TunnelDirections.NORTH:
					return Vector3.up * gravMultiplier;
				case TunnelDirections.SOUTH:
					return Vector3.down * gravMultiplier;
				case TunnelDirections.EAST:
					return Vector3.back * gravMultiplier;
				case TunnelDirections.WEST:
					return Vector3.forward * gravMultiplier;
				default:
					return Vector3.up;
			}
		}

		public void Start() {
			controller = GetComponent<CharacterController>();
		}


		private void Update() {
			Move();

			// Debug.DrawLine(transform.position, transform.position + Vector3.Cross(Vector3.right, GravityVector() * 10),
			// 	Color.red);
			// Debug.DrawLine(transform.position,
			// 	transform.position + -Vector3.Cross(Vector3.right, GravityVector()) * 10,
			// 	Color.blue);
		}

//Calculate orthogonal vectors to the gravity and forward to get an "error" of how off-center the slime is 
		private float CenterError(ref Vector3 vec) {
			Vector3 crossRight = Vector3.Cross(Vector3.right, GravityVector()).normalized;
			Vector3 crossLeft = -crossRight;
			// Does the ray intersect any objects excluding the player layer
			Vector3 position = transform.position;
			Physics.Raycast(position, crossRight, out _rightHit, Mathf.Infinity);
			Physics.Raycast(position, crossLeft, out _leftHit, Mathf.Infinity);
			crossRight = crossRight * _rightHit.distance;
			crossLeft = crossLeft * _leftHit.distance;
			vec = crossLeft + crossRight;

			return vec.magnitude / (crossLeft.magnitude + crossRight.magnitude);
		}


		private void Move() {
			_moveVector = Direction;
			// _moveVector = Vector3.zero;
			if (!IsGrounded) {
				_moveVector += GravityVector();
			}


			if (CenterError(ref errorDrift) > driftMargin) {
				_moveVector += errorDrift;
			}


			Debug.DrawLine(transform.position, transform.position + errorDrift, Color.magenta);
			Debug.DrawLine(transform.position, transform.position + GravityVector() * 10, Color.green);
			controller.Move(_moveVector.normalized * (speed * Time.deltaTime));
		}
	}
}