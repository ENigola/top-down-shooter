using UnityEngine.UI;
using UnityEngine;

public class Highscores : MonoBehaviour {

	private void OnEnable() {
		LoadHighscore(1);
	}

	public void LoadHighscore(int level) {
		for (int i = 1; i <= 10; i++) {
			Text rankLabel = GameObject.Find("Rank" + i).GetComponent<Text>();
			Text timeLabel = GameObject.Find("Time" + i).GetComponent<Text>();
			Text nameLabel = GameObject.Find("Name" + i).GetComponent<Text>();
			if (PlayerPrefs.HasKey("highscore" + i + "level" + level)) {
				string[] entry = PlayerPrefs.GetString("highscore" + i + "level" + level).Split(';');
				rankLabel.text = i + ".";
				timeLabel.text = entry[1];
				nameLabel.text = entry[0];
			} else {
				rankLabel.text = "- - -";
				timeLabel.text = "- - -";
				nameLabel.text = "- - -";
			}
		}
		GameObject.Find("TextHighscoreLevel").GetComponent<Text>().text = "Level " + level;
	}
}
