using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public GameObject cardPrefab;

	public Color[] difficultyBackgrounds;

	public CardCollectionSO cardCollection;
	public GameObject playArea;

	GameState gameState;
	/*
	CardSelection
	PairSelection
	Memorize
	Matching
	EndGame - Game results and menu
	Menu - Game start in here
	*/

	// Start is called before the first frame update
	void Start()
    {
		Difficulty difficulty = Difficulty.EASY; // (Difficulty) PlayerPrefs.GetInt("difficulty", (int) Difficulty.EASY);

		CardGridGenerator cardGenenrator = new CardGridGenerator(cardPrefab, difficultyBackgrounds, cardCollection, difficulty);

		cardGenenrator.Generate(playArea.transform);

	}


}
