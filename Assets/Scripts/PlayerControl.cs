using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

	public float moveSpeed;
    public int maxHp;

    private int currentWeaponIndex;
    private int currentHp;
    private bool invluneralble;
	private float levelStartTime;

    GameObject healthBar;

	void Start() {
		currentWeaponIndex = 0;
        currentHp = maxHp;
        healthBar = GameObject.Find("HealthBarFG");
        invluneralble = false;
		levelStartTime = Time.realtimeSinceStartup;
    }

	void Update () {
		// Movement
		transform.Translate(new Vector2(moveSpeed, 0) * Input.GetAxis("Horizontal") * Time.deltaTime, Space.World);
		transform.Translate(new Vector2(0, moveSpeed) * Input.GetAxis("Vertical") * Time.deltaTime, Space.World);
		// Looking direction
		Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 lookDirection = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
		transform.right = lookDirection;
		// Weapon switching
		if (Input.GetButtonDown("Select 1")) {
			currentWeaponIndex = 0;
		} else if (Input.GetButtonDown("Select 2")) {
			currentWeaponIndex = 1;
		} else if (Input.GetButtonDown("Select 3")) {
			currentWeaponIndex = 2;
		}
		int i = 0;
		foreach (Transform child in transform) {
			if (currentWeaponIndex == i) {
				child.gameObject.SetActive(true);
			} else {
				child.gameObject.SetActive(false);
			}
			i++;
		}
		// Shooting
		if (Input.GetButtonDown("Fire")) {
			GetCurrentGun().StartShooting();
		}
		if (Input.GetButtonUp("Fire")) {
			GetCurrentGun().StopShooting();
		}
		// Reloading
		if (Input.GetButtonDown("Reload")) {
			GetCurrentGun().Reload();
		}
	}

	public float GetLevelStartTime() {
		return levelStartTime;
	}

	public Gun GetCurrentGun() {
		int i = 0;
		foreach (Transform child in transform) {
			if (currentWeaponIndex == i) {
				return child.gameObject.GetComponent<Gun>();
			}
			i++;
		}
		return null;
	}

    public void TakeDamage(int damage)
    {
        if (invluneralble) return;

        // Invulnerability period
        StartCoroutine(GoInvulnerable());
        
        currentHp -= damage;

        // Show change on the HP bar
        float healthBarLength = (float)((float)currentHp / (float)maxHp * 200.0);
        healthBar.GetComponent<RectTransform>().sizeDelta = new Vector2(healthBarLength, 25);

        if (currentHp <= 0)
        { 
            currentHp = 0;
            Debug.Log("you died");
            //TODO death
        }
    }

    private IEnumerator GoInvulnerable()
    {
        invluneralble = true;
        yield return new WaitForSeconds(.25f);
        invluneralble = false;
    }
}
