using UnityEngine;
using UnityEngine.UI;

public class UIUpdate : MonoBehaviour {
	private GameObject player;
	private Text ammoText;
	private Text timeText;

	void Start() {
		player = GameObject.Find("Player");
		ammoText = GameObject.Find("AmmoValue").GetComponent<Text>();
		timeText = GameObject.Find("TimeValue").GetComponent<Text>();
	}

	void Update () {
		// Ammo
		Gun currentGun = player.GetComponent<PlayerControl>().GetCurrentGun();
		if (currentGun.GetIsReloading()) {
			ammoText.text = "RELOADING";
		} else {
			ammoText.text = currentGun.GetClipLeft() + "/" + currentGun.clipSize;
		}
		// Time
		float currentTime = Time.realtimeSinceStartup;
		float time = currentTime - player.GetComponent<PlayerControl>().GetLevelStartTime();
		timeText.text = time.ToString().Split('.')[0];
	}
}
