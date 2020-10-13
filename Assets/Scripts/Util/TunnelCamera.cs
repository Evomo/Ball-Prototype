using Character;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Util {
	public class TunnelCamera : MonoBehaviour {
		[SerializeField] private Slime slime;

		private float _oldFieldOfView;
		private Vector3 _oldPosition;
		[SerializeField] private Camera cam;

		void Start() {
			cam = GetComponent<Camera>();
			slime = FindObjectOfType<Slime>();
		}


		// Update is called once per frame
		// void Update() {
		// 	_oldPosition = new Vector2(transform.position.x, transform.position.y);
		// 	// Vector3 screenMidPoint = slime.currentSegment.next.next.transform.position;
		// 	Vector3 pos = screenMidPoint + (slime.transform.position - screenMidPoint) / 2.0f;
		//
		//
		// 	pos = _oldPosition + (pos - _oldPosition) * 0.2f;
		// 	// transform.position = new Vector3(pos.x, pos.y, transform.position.z);
		//
		// 	transform.LookAt(new Vector3(screenMidPoint.x, screenMidPoint.y, 2000));
		//
		// 	if (slime.controller.isGrounded) {
		// 		cam.fieldOfView = 90 + (_oldFieldOfView - 90) * 0.9f;
		// 		_oldFieldOfView = cam.fieldOfView;
		// 	}
		// 	else {
		// 		cam.fieldOfView = 95 + (_oldFieldOfView - 95) * 0.9f;
		// 		_oldFieldOfView = cam.fieldOfView;
		// 	}
		// }
	}
}