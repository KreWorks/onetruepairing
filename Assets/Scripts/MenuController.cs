using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
	public AudioSource audioSource;

	void Start()
	{
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
		SetGamePrefabs(4, 3, "Easy", "large");
		LoadGameScene();
	}

	public void PlayNormalGame()
	{
		SetGamePrefabs(5, 4, "Normal", "normal");
		LoadGameScene();
	}

	public void PlayHardGame()
	{
		SetGamePrefabs(6, 5, "Hard", "small");
		LoadGameScene();
	}

	void SetGamePrefabs(int x, int y, string difficulty, string spriteSize)
	{
		PlayerPrefs.SetInt("sizeX", x);
		PlayerPrefs.SetInt("sizeY", y);
		PlayerPrefs.SetString("Difficulty", difficulty);
		PlayerPrefs.SetString("SpriteSize", spriteSize);
	}

	void LoadGameScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void QuitGame()
	{
		Application.Quit();
	}

	public void PlayClickSound()
	{
		audioSource.PlayOneShot(Resources.Load<AudioClip>("Sounds/katt"));
	}

}
