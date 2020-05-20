using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
	[Header("Audio Objects")]
	public AudioSource audioSource;

	[Header("Menu Objects")]
	public GameObject background;
	public GameObject mainMenu;
	public GameObject playMenu;
	public GameObject highScoreMenu;
	public GameObject setHighscoreMenu;

	[Header("Timer Objects")]
	public TextMeshProUGUI timerText;
	float timerCount;


	bool isInMenu = false;

	//public GameObject highScoreLine;

	void Start()
	{
		//Reset Menu
		ResetMenu();

		//Reset timer
		timerCount = 0.0f;

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

	void Update()
	{
		if (!isInMenu)
		{
			timerCount += Time.deltaTime;
			timerText.text = Mathf.FloorToInt(timerCount).ToString();
		}	
	}

	private void ResetMenu()
	{
		background.SetActive(true);
		mainMenu.SetActive(true);
		isInMenu = true;
		playMenu.SetActive(false);
		highScoreMenu.SetActive(false);
		setHighscoreMenu.SetActive(false);
	}

	void ExitMenu()
	{
		isInMenu = false;
		background.SetActive(false);
		mainMenu.SetActive(false);
		playMenu.SetActive(false);
		highScoreMenu.SetActive(false);
		setHighscoreMenu.SetActive(false);
	}

	public void PlayEasyGame()
	{
		SetGamePrefabs(4, 3, "Easy", "large");
	}

	public void PlayNormalGame()
	{
		SetGamePrefabs(5, 4, "Normal", "normal");
	}

	public void PlayHardGame()
	{
		SetGamePrefabs(6, 5, "Hard", "small");
	}

	void SetGamePrefabs(int x, int y, string difficulty, string spriteSize)
	{
		PlayerPrefs.SetInt("sizeX", x);
		PlayerPrefs.SetInt("sizeY", y);
		PlayerPrefs.SetString("Difficulty", difficulty);
		PlayerPrefs.SetString("SpriteSize", spriteSize);
		//Exit all menu
		ExitMenu();
		//TODO start game
		CardManager cardManager = FindObjectOfType<CardManager>();
		cardManager.StartGame();
	}	

	public void PlayClickSound()
	{
		audioSource.PlayOneShot(Resources.Load<AudioClip>("Sounds/katt"));
	}

	public void QuitGame()
	{
		Application.Quit();
	}

}
