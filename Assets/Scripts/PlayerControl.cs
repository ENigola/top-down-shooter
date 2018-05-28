using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

	private int currentWeaponIndex;
	public float moveSpeed;

	void Start() {
		currentWeaponIndex = 0;
	}

	void Update () {
		// Movement
		transform.Translate(new Vector2(moveSpeed, 0) * Input.GetAxis("Horizontal") * Time.deltaTime, Space.World);
		transform.Translate(new Vector2(0, moveSpeed) * Input.GetAxis("Vertical") * Time.deltaTime, Space.World);
		// Looking direction
		Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 lookDirection = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
		transform.right = lookDirection;
		// Weapon switching
		if (Input.GetButtonDown("Select 1")) {
			currentWeaponIndex = 0;
		} else if (Input.GetButtonDown("Select 2")) {
			currentWeaponIndex = 1;
		} else if (Input.GetButtonDown("Select 3")) {
			currentWeaponIndex = 2;
		}
		int i = 0;
		foreach (Transform child in transform) {
			if (currentWeaponIndex == i) {
				child.gameObject.SetActive(true);
			} else {
				child.gameObject.SetActive(false);
			}
			i++;
		}
		// Shooting
		if (Input.GetButtonDown("Fire")) {
			GetCurrentGun().StartShooting();
		}
		if (Input.GetButtonUp("Fire")) {
			GetCurrentGun().StopShooting();
		}
	}

	private Gun GetCurrentGun() {
		int i = 0;
		foreach (Transform child in transform) {
			if (currentWeaponIndex == i) {
				return child.gameObject.GetComponent<Gun>();
			}
			i++;
		}
		return null;
	}
}
