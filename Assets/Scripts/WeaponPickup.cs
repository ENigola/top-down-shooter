using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour {

    public int gunId;

    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.GetComponent<PlayerControl>().UnlockGun(gunId);
            Destroy(gameObject);
        }
    }

}
