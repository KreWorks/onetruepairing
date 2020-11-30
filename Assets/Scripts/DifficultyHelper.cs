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

	public static Vector2 GetPlayAreaSize(Difficulty difficulty)
	{
		switch (difficulty)
		{
			case Difficulty.EASY:
				return new Vector2(4, 3);
			case Difficulty.NORMAL:
				return new Vector2(5, 4);
			case Difficulty.HARD:
				return new Vector2(6, 5);
			default:
				return new Vector2(4, 3);
		}
	}
}
