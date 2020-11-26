using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScoreEntry 
{
	public string userName;
	public int points;
}

public class HighScores
{
	public List<ScoreEntry> entryList;

	public HighScores()
	{
		entryList = new List<ScoreEntry>();
	}

	public void SortList()
	{
		List<ScoreEntry> orderedList = new List<ScoreEntry>();

		while(entryList.Count > 0)
		{
			int maxScore = 0;
			ScoreEntry maxScoreEntry = new ScoreEntry();
			foreach(ScoreEntry se in entryList)
			{
				if(se.points > maxScore)
				{
					maxScore = se.points;
					maxScoreEntry = se;
				}
			}
			entryList.Remove(maxScoreEntry);
			orderedList.Add(maxScoreEntry);
		}

		entryList = orderedList;
	}
}