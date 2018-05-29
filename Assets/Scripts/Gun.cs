using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour {
	public GameObject bulletPrefab;
	public int damage;
	public float fireRate; // Shots per second
	public float bulletSpeed;
	public int clipSize;
	public float reloadTime; // In seconds
	private float gunEndOffset = 0.7f;

	protected float lastFireTime = -1000;
	private int clipLeft;
	private bool isReloading = false;
	private float reloadStart;

	private void Awake() {
		clipLeft = clipSize;
	}

	public abstract void StartShooting();

	public abstract void StopShooting();

	protected void TryToFire() {
		if (isReloading) {
			CheckReload();
		}
		if (isReloading) {
			return;
		}
		if (clipLeft == 0) {
			Reload();
			return;
		}
		float fireInterval = 1 / fireRate;
		if (Time.realtimeSinceStartup - lastFireTime > fireInterval) {
			Fire();
			lastFireTime = Time.realtimeSinceStartup;
			clipLeft--;
			if (clipLeft == 0) {
				Reload();
			}
		}
	}

	private void Fire() {
		Bullet bullet = GameObject.Find("Player").GetComponent<Pool>().GetObject().GetComponent<Bullet>();
		bullet.SetDamage(damage);
		bullet.SetSpeed(bulletSpeed);
		bullet.transform.SetPositionAndRotation(transform.position, transform.rotation);
		bullet.transform.Translate(Vector2.right * gunEndOffset);
	}
	
	public void Reload() {
		if (!isReloading) {
			reloadStart = Time.realtimeSinceStartup;
			isReloading = true;
		}
	}

	private void CheckReload() {
		if (Time.realtimeSinceStartup - reloadStart > reloadTime) {
			clipLeft = clipSize;
			isReloading = false;
		}
	}

	private void OnDisable() {
		isReloading = false;
		StopShooting();
	}

	private void OnEnable() {
		if (clipLeft == 0) {
			Reload();
		}
	}
}
