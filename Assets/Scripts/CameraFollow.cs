using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	private float cameraOffsetZ = -10;

	void Start () {
	}
	
	void Update () {
		Camera.main.transform.position = transform.position + new Vector3(0, 0, cameraOffsetZ);
	}
}
