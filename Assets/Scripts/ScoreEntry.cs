using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScoreEntry : IComparable
{
	public string userName;
	public int score;

	public ScoreEntry(string userName, int score)
	{
		this.userName = userName;
		this.score = score;
	}

	public static bool operator < (ScoreEntry left, ScoreEntry right)
	{
		return left.score < right.score;
	}

	public static bool operator > (ScoreEntry left, ScoreEntry right)
	{
		return left.score > right.score;
	}

	public static bool operator <= (ScoreEntry left, ScoreEntry right)
	{
		return left.score <= right.score;
	}

	public static bool operator >= (ScoreEntry left, ScoreEntry right)
	{
		return left.score >= right.score;
	}

	public static bool operator == (ScoreEntry left, ScoreEntry right)
	{
		return left.score == right.score;
	}

	public static bool operator != (ScoreEntry left, ScoreEntry right)
	{
		return left.score != right.score;
	}

	public override bool Equals(object obj)
	{
		ScoreEntry se = (ScoreEntry)obj;
 		return base.Equals(obj);
	}

	public override int GetHashCode()
	{
		return base.GetHashCode();
	}

	public override string ToString()
	{
		return this.userName + " : " + this.score.ToString();
	}

	public int CompareTo(object obj)
	{
		ScoreEntry se = (ScoreEntry)obj;
		return this.score - se.score;
	}
}
