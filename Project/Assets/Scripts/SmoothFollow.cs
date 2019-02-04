using UnityEngine;

namespace UnityStandardAssets.Utility {
	public class SmoothFollow : MonoBehaviour {
		public Transform target;
		public float distance = 7.0f;
		public float height = 1.25f;
        public float rotationDamping = 10.0f;

		void LateUpdate() {
			if (!target) return;

			//Calculate the current rotation angles. 현재의 각도 값을 계산한다.
			var wantedRotationAngle = target.eulerAngles.y;
			var wantedHeight = target.position.y + height;

			var currentRotationAngle = transform.eulerAngles.y;

			// Damp the rotation around the y-axis
			currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);


			// Convert the angle into a rotation
			var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

			// Set the position of the camera on the x-z plane to:
			// distance meters behind the target
			transform.position = target.position;
			transform.position -= currentRotation * Vector3.forward * distance; //타겟 뒤쪽에 distance만큼 떨어져 위치.


			transform.position = new Vector3(transform.position.x, wantedHeight, transform.position.z);
            
			transform.LookAt(target);
		}
	}
}