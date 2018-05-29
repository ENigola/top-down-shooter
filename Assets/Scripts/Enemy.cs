using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	public int maxHp;
	public float moveSpeed;
	public int damage;
	private int currentHp;

	private void Start() {
		currentHp = maxHp;
	}

	private void Update() {
		// TODO: movement
	}

	public void DoDamage(int damage) {
		currentHp -= damage;
		if (currentHp <= 0) {
			Die();
		}
		// TODO: start chasing player if not already
	}

	private void Die() {
		Destroy(gameObject);
	}
}
