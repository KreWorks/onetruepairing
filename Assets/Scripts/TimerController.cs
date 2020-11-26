using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{
	public TMP_Text inGameTimeShower;
	public TMP_Text endGameText;

	float gameTime;
	bool isInGame;

	public float GameTime
	{
		set
		{
			gameTime = value;
		}
		get
		{
			return gameTime;
		}
	}

	// Start is called before the first frame update
	void Start()
    {
		gameTime = 0.0f;
		isInGame = true;
    }

    // Update is called once per frame
    void Update()
    {
		if (isInGame)
		{
			gameTime += Time.deltaTime;
			inGameTimeShower.text = GetTimeString();
		}
    }

	string GetTimeString()
	{
		int min = Mathf.FloorToInt(gameTime / 60.0f);
		int sec = Mathf.FloorToInt(gameTime % 60);

		return (GetNumberWithLeadingZero(min) + ":" + GetNumberWithLeadingZero(sec));
	}

	string GetNumberWithLeadingZero(int min)
	{
		string timeString = "";

		if (min < 10)
		{
			timeString += "0";
		}

		timeString += min.ToString();

		return timeString;
	}

	public void SetEndGameText()
	{
		endGameText.text = "You finished in " + GetTimeString() + "!";
	}

	public void PauseGame()
	{
		isInGame = false;
	}

	public void ResumeGame()
	{
		isInGame = true;
	}
}
