using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	private int damage;
	private float speed;

	void Update () {
		transform.Translate(Vector2.right * speed * Time.deltaTime);
	}

	public void SetSpeed(float speed) {
		this.speed = speed;
	}

	public void SetDamage(int damage) {
		this.damage = damage;
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		if (!gameObject.activeSelf) {
			// To fix double collision problems
			return;
		}
		if (collision.gameObject.tag == "Enemy") {
			collision.gameObject.GetComponent<Enemy>().DoDamage(damage);
		}
		GameObject.Find("Player").GetComponent<Pool>().ReleaseObject(gameObject);
	}
}
