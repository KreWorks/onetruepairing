using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuController : MonoBehaviour
{
	public GameObject mainMenuPanel;
	public GameObject playMenuPanel;
	public GameObject highScoreMenuPanel;

	public GameObject highScoreUIPrefab;
	public GameObject highScoreList;

	public GameObject easyTab;
	public GameObject normalTab;
	public GameObject hardTab;

	public Color activeColor;
	public Color inactiveColor;

	HighScores easyHighScores;
	HighScores normalHighScores;
	HighScores hardHighScores;

	void Awake()
	{
		playMenuPanel.SetActive(false);
		highScoreMenuPanel.SetActive(false);
	}

	void Start()
	{
		easyHighScores = HighScoreHelper.LoadHighScores(Difficulty.EASY);
		normalHighScores = HighScoreHelper.LoadHighScores(Difficulty.NORMAL);
		hardHighScores = HighScoreHelper.LoadHighScores(Difficulty.HARD);

		SwitchHighScoreTab(PlayerPrefs.GetInt("Difficulty"));
	}

	public void SwitchHighScoreTab(int difficulty)
	{
		HighScores highScores = GetHighScoresValueByDifficulty((Difficulty)difficulty);

		ChangeTabLabel((Difficulty)difficulty);

		ChangeHighScoreList(highScores);
	}

	public void PlayGame(int difficulty)
	{
		PlayerPrefs.SetInt("Difficulty", difficulty);

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void QuitGame()
	{
		Application.Quit();
	}

	HighScores GetHighScoresValueByDifficulty(Difficulty difficulty)
	{
		switch (difficulty)
		{
			case Difficulty.EASY:
				return easyHighScores;
			case Difficulty.NORMAL:
				return normalHighScores;
			case Difficulty.HARD:
				return hardHighScores;
			default:
				return normalHighScores;
		}
	}
	
	void ChangeTabLabel(Difficulty difficulty)
	{
		easyTab.GetComponent<Image>().color = inactiveColor;
		normalTab.GetComponent<Image>().color = inactiveColor;
		hardTab.GetComponent<Image>().color = inactiveColor;

		switch (difficulty)
		{
			case Difficulty.EASY:
				easyTab.GetComponent<Image>().color = activeColor;
				break;
			case Difficulty.NORMAL:
				normalTab.GetComponent<Image>().color = activeColor;
				break;
			case Difficulty.HARD:
				hardTab.GetComponent<Image>().color = activeColor;
				break;
			default:
				normalTab.GetComponent<Image>().color = activeColor;
				break;
		}
	}

	void ChangeHighScoreList(HighScores highScores)
	{
		foreach(Transform child in highScoreList.GetComponentsInChildren<Transform>())
		{
			if(child != highScoreList.transform)
			{
				Destroy(child.gameObject);
			}
		}

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
					text.text = highScore.score.ToString();
				}
			}
		}
	}
}
