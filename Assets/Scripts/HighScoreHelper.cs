using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HighScoreHelper
{
	public static int entryListSize = 5;

    public static HighScores LoadHighScores()
	{
		HighScores scoreList = new HighScores();

		string scoreJson = PlayerPrefs.GetString("HighScores", JsonUtility.ToJson(scoreList));
		scoreList = JsonUtility.FromJson<HighScores>(scoreJson);

		return scoreList;
	}

	public static void SaveHighScore(HighScores scoreList)
	{
		string json = JsonUtility.ToJson(scoreList);

		PlayerPrefs.SetString("HighScores", json);
		PlayerPrefs.Save();
	}

	public static HighScores AddHighScore(HighScores highScores, ScoreEntry scoreEntry)
	{
		HighScores limitedList = new HighScores(); 

		highScores.entryList.Add(scoreEntry);
		
		while(highScores.entryList.Count > entryListSize)
		{
			int minScore = int.MaxValue;
			ScoreEntry minScoreEntry = null;

			foreach(ScoreEntry se in highScores.entryList)
			{
				if(minScore > se.points)
				{
					minScore = se.points;
					minScoreEntry = se;
				}
			}

			highScores.entryList.Remove(minScoreEntry);
		}

		limitedList.entryList = highScores.entryList;
		limitedList.SortList();

		return limitedList;
	}

	public static int CalculateHighScore(int time, int movesCount)
	{
		int score = 0;

		return score;
	}
}
