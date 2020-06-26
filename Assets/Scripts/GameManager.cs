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


	// Start is called before the first frame update
	void Start()
    {
		Difficulty difficulty = (Difficulty) PlayerPrefs.GetInt("difficulty", (int) Difficulty.EASY);


		CardGridGenerator cardGenenrator = new CardGridGenerator(transform.position, cardPrefab, difficultyBackgrounds, cardCollection, difficulty);

		cardGenenrator.Generate(playArea.transform);

	}


}
