using UnityEngine;
using TMPro;

public class MenuController : MonoBehaviour
{
	public GameObject mainMenuPanel;
	public GameObject playMenuPanel;
	public GameObject highScoreMenuPanel;

	public GameObject highScoreUIPrefab;
	public GameObject highScoreList;

	void Awake()
	{
		playMenuPanel.SetActive(false);
		highScoreMenuPanel.SetActive(false);
	}

	void Start()
	{
		HighScores highScores = HighScoreHelper.LoadHighScores();

		Debug.Log("HighScore count: " + highScores.entryList.Count);

		foreach (ScoreEntry highScore in highScores.entryList)
		{
			GameObject hs = Instantiate(highScoreUIPrefab, highScoreList.transform);
			TMP_Text[] childs = hs.GetComponentsInChildren<TMP_Text>();

			foreach (TMP_Text text in childs)
			{
				if (text.gameObject.name == "Name")
				{
					text.text = highScore.userName;
				}
				else if (text.gameObject.name == "Score")
				{
					text.text = highScore.points.ToString();
				}
			}
		}
	}

	public void QuitGame()
	{
		Application.Quit();
	}

}
