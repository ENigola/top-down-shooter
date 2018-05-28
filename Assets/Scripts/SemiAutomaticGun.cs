using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemiAutomaticGun : Gun {

	public override void StartShooting() {
		TryToFire();
	}

	public override void StopShooting() {
		return;
	}
}
