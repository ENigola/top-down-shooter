using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

	private int currentWeapon;
	public float moveSpeed;
	
	void Update () {
		// Movement
		transform.Translate(new Vector2(moveSpeed, 0) * Input.GetAxis("Horizontal") * Time.deltaTime, Space.World);
		transform.Translate(new Vector2(0, moveSpeed) * Input.GetAxis("Vertical") * Time.deltaTime, Space.World);
		// Looking direction
		Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 lookDirection = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
		transform.right = lookDirection;
		// Weapon switching
		if (Input.GetAxis("Select 1") != 0f) {
			currentWeapon = 0;
		} else if (Input.GetAxis("Select 2") != 0f) {
			currentWeapon = 1;
		} else if (Input.GetAxis("Select 3") != 0f) {
			currentWeapon = 2;
		}
		int i = 0;
		foreach (Transform child in transform) {
			if (currentWeapon == i) {
				child.gameObject.SetActive(true);
			} else {
				child.gameObject.SetActive(false);
			}
			i++;
		}
	}
}
