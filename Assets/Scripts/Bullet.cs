using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	private float damage;
	private float speed;

	void Update () {
		transform.Translate(Vector2.right * speed * Time.deltaTime);
	}

	public void SetSpeed(float speed) {
		this.speed = speed;
	}

	public void SetDamage(float damage) {
		this.damage = damage;
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "Enemy") {
			// TODO: do damage
		}
	}
}
