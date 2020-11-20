using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuController : MonoBehaviour
{
	void Start()
	{
		//Reset HighScore
		string[] startNames = { "Bence", "Zita", "Péter", "Franciska", "Lukrécia" };

		if (PlayerPrefs.GetInt("Score1", 0) == 0)
		{
			for (int i = 1; i <= 5; i++)
			{
				PlayerPrefs.SetString("Name" + i.ToString(), startNames[i - 1]);
				PlayerPrefs.SetInt("Score" + i.ToString(), (6 - i) * 50);
			}
		}
	}

	public void PlayEasyGame()
	{
		SetGamePrefabs((int) Difficulty.EASY);
	}

	public void PlayNormalGame()
	{
		SetGamePrefabs((int) Difficulty.NORMAL);
	}

	public void PlayHardGame()
	{
		SetGamePrefabs((int) Difficulty.HARD);
	}

	void SetGamePrefabs(int difficulty)
	{
		PlayerPrefs.SetInt("difficulty", difficulty);

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}	

	public void QuitGame()
	{
		Application.Quit();
	}

}
