using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{
	public TMP_Text timer;

	float gameTime;

	// Start is called before the first frame update
	void Start()
    {
		gameTime = 0.0f;    
    }

    // Update is called once per frame
    void Update()
    {
		gameTime += Time.deltaTime;
		timer.text = GetTimeString();
    }

	string GetTimeString()
	{
		int min = Mathf.FloorToInt(gameTime / 60.0f);
		int sec = Mathf.FloorToInt(gameTime % 60);

		string timeString = "";
		if (min < 10)
		{
			timeString += "0";
		}
		timeString += min.ToString() + ":";

		if (sec < 10)
		{
			timeString += "0";
		}
		timeString += sec.ToString();

		return timeString;
	}
}
