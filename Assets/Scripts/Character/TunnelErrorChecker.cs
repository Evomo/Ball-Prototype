using UnityEngine;
using Util;

namespace Character {
	public class TunnelErrorChecker : MonoBehaviour {
		private Vector3 _gravityVec;
		private RaycastHit _rightHit, _leftHit;
		protected Vector3 ErrorDrift;

		public TunnelDirection currGravityDirection;
		[Range(1, 20)] public float gravMultiplier;

		[Range(0, 1), Tooltip("Represents the percentage of being 'off-center'")]
		public float driftCorrectionThreshold;


		protected float CurrentDriftError => CenterError(out ErrorDrift);
		protected bool DriftPassesThreshold => CurrentDriftError > driftCorrectionThreshold;

		protected void ShowDebugVectors() {
			Debug.DrawLine(transform.position, transform.position + Vector3.Cross(Vector3.right, GravityVector() * 10),
				Color.red);
			Debug.DrawLine(transform.position,
				transform.position + -Vector3.Cross(Vector3.right, GravityVector()) * 10,
				Color.blue);
		}

		protected Vector3 GravityVector() {
			switch (currGravityDirection) {
				case TunnelDirection.NORTH:
					_gravityVec = Vector3.up;
					break;
				case TunnelDirection.SOUTH:
					_gravityVec = Vector3.down;
					break;
				case TunnelDirection.EAST:
					_gravityVec = Vector3.back;
					break;
				case TunnelDirection.WEST:
					_gravityVec = Vector3.forward;
					break;
				default:
					_gravityVec = Vector3.up;
					break;
			}

			return _gravityVec * gravMultiplier;
		}


//Calculate orthogonal vectors to the gravity and forward to get an "error" of how off-center the slime is 
		protected float CenterError(out Vector3 vec) {
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
	}
}