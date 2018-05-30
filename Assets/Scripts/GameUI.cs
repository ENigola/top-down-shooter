using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour {
	private GameObject player;
	private Text ammoText;
	private Text timeText;
	public GameObject playerDeadPanel;
	public GameObject levelCompletePanel;

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
	}

	public void RestartLevel() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void LoadMenu() {
		SceneManager.LoadScene("Menu");
	}

	public void SubmitScore() {
		string playerName = GameObject.Find("PlayerName").GetComponent<Text>().text;
		//PlayerPrefs
		Debug.Log(playerName);
		SceneManager.LoadScene("Menu");
	}
}
