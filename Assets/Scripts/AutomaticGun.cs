using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticGun : Gun {
	private bool isShooting = false;

	private void Update() {
		if (isShooting) {
			TryToFire();
		}
	}

	public override void StartShooting() {
		isShooting = true;
	}

	public override void StopShooting() {
		isShooting = false;
	}
}
