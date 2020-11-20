using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Card Game Objects/Card")]
public class CardSO : ScriptableObject
{
	public string cardName;
	public string pairName;
	public Sprite cardImage;

	public bool IsPair(string givenName)
	{
		givenName = givenName.ToLower();

		return (givenName == pairName);
	}
}
