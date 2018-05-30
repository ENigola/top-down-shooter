using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	public int maxHp;
	public float moveSpeed;
	public int damage;

	private int currentHp;
    private bool chasing;
    private bool randomWalking;
    private bool randomTimeout;
    private Vector2 randomDirection;

    public GameObject player;

	private void Start() {
		currentHp = maxHp;
        chasing = false;
        randomWalking = false;
        randomTimeout = false;
		if (player == null) {
			player = GameObject.Find("Player");
		}
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
            transform.right = player.transform.position - transform.position;
        }
        else
        {
            // If enemy can see the player, start chasing the player
            RaycastHit2D hit = Physics2D.Raycast(transform.position, (player.transform.position - transform.position));
            if (hit.collider != null)
            {
                if (hit.transform == player.transform) //TODO Maybe see through other enemies?
                {
                    chasing = true;
                }
            }

            // Walk around randomly
            if (!randomWalking && !randomTimeout)
            {
                randomDirection = Random.insideUnitCircle;
                randomDirection.Normalize();
                StartCoroutine(WalkInRandomDirectionTimer());
            }
            else if (randomWalking)
            {
                transform.Translate(new Vector2(0, moveSpeed/2f) * randomDirection.y * Time.deltaTime, Space.World);
                transform.Translate(new Vector2(moveSpeed/2f, 0) * randomDirection.x * Time.deltaTime, Space.World);
                transform.right = randomDirection;
            }

        } 

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
		if (GameObject.Find("Enemies").transform.childCount == 1) {
			GameObject.Find("Game UI Canvas").GetComponent<GameUI>().ShowLevelComplete();
		}
		Destroy(gameObject);
	}

    private IEnumerator WalkInRandomDirectionTimer()
    {
        randomWalking = true;
        yield return new WaitForSeconds(Random.Range(0.0f, 1.0f) * 5);
        randomTimeout = true;
        randomWalking = false;
        yield return new WaitForSeconds(Random.Range(0.0f, 1.0f) * 5);
        randomTimeout = false;
    }
}
