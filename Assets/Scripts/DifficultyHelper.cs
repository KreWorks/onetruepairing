using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DifficultyHelper 
{
	public static string GetDifficultyString(Difficulty difficulty)
	{
		switch (difficulty)
		{
			case Difficulty.EASY:
				return "easy";
			case Difficulty.NORMAL:
				return "normal";
			case Difficulty.HARD:
				return "hard";
			default:
				return "easy";
		}
	}
	public static string GetIconSizeByDifficulty(Difficulty difficulty)
	{
		switch (difficulty)
		{
			case Difficulty.EASY:
				return "large";
			case Difficulty.NORMAL:
				return "normal";
			case Difficulty.HARD:
				return "small";
			default:
				return "large";
		}
	}
}
