using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HighScoreHelper
{
	public static int entryListSize = 5;

	public static string easyPlayerPrefabLabel = "EasyHighScores";
	public static string normalPlayerPrefabLabel = "NormalHighScores";
	public static string hardPlayerPregfabLabel = "HardHighScores";

	public static string GetHighScorelabelByDifficulty(Difficulty difficulty)
	{
		switch (difficulty)
		{
			case Difficulty.EASY:
				return easyPlayerPrefabLabel;
			case Difficulty.NORMAL:
				return normalPlayerPrefabLabel;
			case Difficulty.HARD:
				return hardPlayerPregfabLabel;
			default:
				return normalPlayerPrefabLabel;
		}
	}

    public static HighScores LoadHighScores(Difficulty difficulty)
	{
		HighScores scoreList = new HighScores();

		string scoreJson = PlayerPrefs.GetString(GetHighScorelabelByDifficulty(difficulty), JsonUtility.ToJson(scoreList));
		scoreList = JsonUtility.FromJson<HighScores>(scoreJson);

		return scoreList;
	}

	public static void SaveHighScore(HighScores scoreList, Difficulty difficulty)
	{
		string json = JsonUtility.ToJson(scoreList);

		PlayerPrefs.SetString(GetHighScorelabelByDifficulty(difficulty), json);
		PlayerPrefs.Save();
	}

	public static HighScores AddHighScore(HighScores highScores, ScoreEntry scoreEntry)
	{
		highScores.entryList.Add(scoreEntry);
		
		while(highScores.entryList.Count > entryListSize && highScores.entryList.Count > 0)
		{
			int minScore = int.MaxValue;
			ScoreEntry minScoreEntry = null;

			foreach(ScoreEntry se in highScores.entryList)
			{
				if(minScore > se.score)
				{
					minScore = se.score;
					minScoreEntry = se;
				}
			}

			highScores.entryList.Remove(minScoreEntry);
		}

		highScores.SortList();

		return highScores;
	}

	public static int CalculateHighScore(int time, int movesCount, Difficulty difficulty)
	{
		int score = GetFullTimeByDifficulty(difficulty) - time;

		score += GetFullTimeByDifficulty(difficulty) / 10 - movesCount;

		return score;
	}

	public static int GetFullTimeByDifficulty(Difficulty difficulty)
	{
		switch (difficulty)
		{
			case Difficulty.EASY:
				return 4 * 60;
			case Difficulty.NORMAL:
				return 5 * 60;
			case Difficulty.HARD:
				return 6 * 60;
			default:
				return 5 * 60;
		}
	}
}
