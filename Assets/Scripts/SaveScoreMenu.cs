using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SaveScoreMenu : MonoBehaviour
{
	public TextMeshProUGUI score;
	public TextMeshProUGUI nameText;

	void Start()
	{
		int scorePoints = PlayerPrefs.GetInt("ActualPoints", 100);
		score.text = scorePoints.ToString() ;
	}

	public void SaveScore()
	{
		int actualScore = int.Parse(score.text);

		int previousScore = 0;
		string previousName = "";
		bool isSet = false;
		for (int i = 1; i <= 5; i++)
		{
			int score = PlayerPrefs.GetInt("Score" + i.ToString());
			if (score <= actualScore && !isSet)
			{
				previousScore = score; 
				previousName = PlayerPrefs.GetString("Name" + i.ToString());

				PlayerPrefs.SetInt("Score" + i.ToString(), actualScore);
				PlayerPrefs.SetString("Name" + i.ToString(), nameText.text);
				isSet = true;
			}else
			{
				if (isSet)
				{
					int s  = PlayerPrefs.GetInt("Score" + i.ToString());
					string n = PlayerPrefs.GetString("Name" + i.ToString());

					PlayerPrefs.SetInt("Score" + i.ToString(), previousScore);
					PlayerPrefs.SetString("Name" + i.ToString(), previousName);

					previousScore = s;
					previousName = n;
				}
			}
		}

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
	}
}
