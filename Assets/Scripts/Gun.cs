using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour {
	public GameObject bulletPrefab;
	public float damage;
	public float fireRate; // Shots per second
	public float bulletSpeed;
	private float gunEndOffset = 0.7f;

	protected float lastFireTime = -1000;

	public abstract void StartShooting();

	public abstract void StopShooting();

	protected void TryToFire() {
		float fireInterval = 1 / fireRate;
		if (Time.realtimeSinceStartup - lastFireTime > fireInterval) {
			Fire();
			lastFireTime = Time.realtimeSinceStartup;
		}
	}

	private void Fire() {
		// TODO: replace with pool
		Bullet bullet = Instantiate(bulletPrefab, transform.position, transform.rotation).GetComponent<Bullet>();
		bullet.SetDamage(damage);
		bullet.SetSpeed(bulletSpeed);
		bullet.transform.SetPositionAndRotation(transform.position, transform.rotation);
		bullet.transform.Translate(Vector2.right * gunEndOffset);
	}
	
}
