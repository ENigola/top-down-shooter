﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	public int maxHp;
	public float moveSpeed;
	public int damage;

	private int currentHp;
    private bool chasing;

    public GameObject player;

	private void Start() {
		currentHp = maxHp;
        chasing = false;
	}

	private void Update() {

        // Calculate player direction
        int chaseY = 0;
        int chaseX = 0;
        if (player.transform.position.y - transform.position.y > 0)
        {
            chaseY = 1;
        }
        else if (player.transform.position.y - transform.position.y < 0)
        {
            chaseY = -1;
        }
        if (player.transform.position.x - transform.position.x > 0)
        {
            chaseX = 1;
        }
        else if (player.transform.position.x - transform.position.x < 0)
        {
            chaseX = -1;
        }

        if (chasing)
        {
            // Movement towards player
            transform.Translate(new Vector2(0, moveSpeed) * chaseY * Time.deltaTime, Space.World);
            transform.Translate(new Vector2(moveSpeed, 0) * chaseX * Time.deltaTime, Space.World);
        }
        else
        {
            // If enemy can see the player, start chasing the player
            Debug.Log("Raycast");
            RaycastHit2D hit = Physics2D.Raycast(transform.position, (player.transform.position - transform.position));
            if (hit.collider != null)
            {
                Debug.Log(hit.transform);
                if (hit.transform == player.transform)
                {
                    chasing = true;
                }
            }
        } // TODO ilmselt peaks midagi tegema ka siis kui ei chase'i

    }

	public void DoDamage(int damage) {
        chasing = true;
        currentHp -= damage;
		if (currentHp <= 0) {
			Die();
		}
	}

    // Damaging the player 
    private void OnCollisionStay2D (Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerControl>().TakeDamage(damage);
        }
    }

    // Damaging the player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerControl>().TakeDamage(damage);
        }
    }

    private void Die() {
		Destroy(gameObject);
	}
}
