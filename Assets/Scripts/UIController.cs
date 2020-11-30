using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
	public GameObject endGamePanel;
	public GameObject pauseGamePanel;

	public TMP_Text countText;

	public TMP_InputField userNameField;

	int movesCount;

    // Start is called before the first frame update
    void Start()
    {
		endGamePanel.SetActive(false);
		pauseGamePanel.SetActive(false);

		countText.text = "0";
    }

	public void ActivateEndPanel()
	{
		endGamePanel.SetActive(true);
	}

	public void ActivatePausePanel()
	{
		pauseGamePanel.SetActive(true);
	}

	public void ChangeMovesCount(int movesCount)
	{
		this.movesCount = movesCount;
		countText.text = movesCount.ToString();
	}

	public void SaveHighScore()
	{
		Difficulty savedDifficulty = (Difficulty)PlayerPrefs.GetInt("Difficulty");

		TimerController timerController = FindObjectOfType<TimerController>();
		string userName = userNameField.text;

		int score = HighScoreHelper.CalculateHighScore(Mathf.FloorToInt(timerController.GameTime), movesCount, savedDifficulty);

		HighScores hs = HighScoreHelper.LoadHighScores(savedDifficulty);
		ScoreEntry newHighScore = new ScoreEntry(userName, score);

		HighScoreHelper.AddHighScore(hs, newHighScore);
		HighScoreHelper.SaveHighScore(hs, savedDifficulty);

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
	}

	public void QuitToMenu()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
	}
}
