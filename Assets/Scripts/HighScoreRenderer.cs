using UnityEngine;
using TMPro;

public class HighScoreRenderer : MonoBehaviour
{
	public TextMeshProUGUI[] names;
	public TextMeshProUGUI[] scores;

	public void LoadHighScores()
	{
		string[] startNames = { "Péter", "Bence", "Zita", "Franciska", "Lukrécia" };

		for (int i = 1; i <= 5; i++)
		{
			string name = PlayerPrefs.GetString("Name" + i.ToString(), startNames[i - 1]);
			int score = PlayerPrefs.GetInt("Score" + i.ToString(), (6-i) * 100);
			int index = i - 1;
			names[index].text = name;
			scores[index].text = score.ToString();
		}
	}
}
