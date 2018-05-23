using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

	public float moveSpeed;
	
	void Update () {
		transform.Translate(new Vector2(moveSpeed, 0) * Input.GetAxis("Horizontal") * Time.deltaTime);
		transform.Translate(new Vector2(0, moveSpeed) * Input.GetAxis("Vertical") * Time.deltaTime);
	}
}
