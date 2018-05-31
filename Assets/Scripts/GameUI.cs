using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameUI : MonoBehaviour {
	private GameObject player;
	private Text ammoText;
	private Text timeText;
	public GameObject playerDeadPanel;
	public GameObject levelCompletePanel;
	private int levelCompleteTime;

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

	public void ShowPlayerDead() {
		playerDeadPanel.SetActive(true);
	}

	public void ShowLevelComplete() {
		levelCompletePanel.SetActive(true);
		levelCompleteTime = (int) (Time.realtimeSinceStartup - player.GetComponent<PlayerControl>().GetLevelStartTime());
	}

	public void RestartLevel() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void LoadMenu() {
		SceneManager.LoadScene("Menu");
	}

	public void SubmitScore() {
		string playerName = GameObject.Find("PlayerName").GetComponent<Text>().text;
		if (playerName == "") {
			playerName = "-";
		}
		playerName.Replace(';', ',');
		string level = SceneManager.GetActiveScene().name.Split(' ')[1];
		// Format example: highscore4level2 = Ergo;38
		for (int i = 1; i <= 10; i++) {
			if (PlayerPrefs.HasKey("highscore" + i + "level" + level)) {
				string[] entry = PlayerPrefs.GetString("highscore" + i + "level" + level).Split(';');
				if (Convert.ToInt32(entry[1]) > levelCompleteTime) {
					for (int j = 10; j > i; j--) {
						if (PlayerPrefs.HasKey("highscore" + (j - 1) + "level" + level)) {
							PlayerPrefs.SetString("highscore" + j + "level" + level, PlayerPrefs.GetString("highscore" + (j - 1) + "level" + level));
						}
					}
					PlayerPrefs.SetString("highscore" + i + "level" + level, playerName + ";" + levelCompleteTime);
					break;
				}
			} else {
				PlayerPrefs.SetString("highscore" + i + "level" + level, playerName + ";" + levelCompleteTime);
				break;
			}
		}
		SceneManager.LoadScene("Menu");
	}
}
