using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScores
{
	public List<ScoreEntry> entryList;

	public HighScores()
	{
		entryList = new List<ScoreEntry>();
	}

	public void SortList()
	{
		if(entryList.Count > 0)
		{
			entryList.Sort((a,b) => b.CompareTo(a));
		}	
	}
}
