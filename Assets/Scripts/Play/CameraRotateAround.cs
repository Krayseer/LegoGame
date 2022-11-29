using UnityEngine;

namespace Play
{
	public class CameraRotateAround : MonoBehaviour
	{
		public Transform target;
		public Vector3 offset;

		public float sensitivity = 1;
		private float X, Y;

		public static CameraRotateAround Instance;
		public static int direction;

		void Start()
		{
			offset = new Vector3(0.88f, -0.49f, -40);
			Instance = this;
		}

		void Update()
		{ 
			if(direction == 1)
				X = transform.localEulerAngles.y + sensitivity * 2;
			if (direction == 2)
				Y += sensitivity;
			if (direction == 3)
				Y -= sensitivity;
			if (direction == 4)
				X = transform.localEulerAngles.y - sensitivity * 2;
			Y = Mathf.Clamp(Y, 0, 90);
			transform.localEulerAngles = new Vector3(Y, X, 0);
			transform.position = transform.localRotation * offset + target.position;
		}
	}
}