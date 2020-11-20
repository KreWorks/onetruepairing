using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
	public GameObject cardPrefab;

	public GameDatasSO easyData;
	public GameDatasSO normalData;
	public GameDatasSO hardData;

	GameDatasSO gameDatas;
	CardGridGenerator cardGridGenerator;


	void Awake()
	{
		GetGameDatasByDifficulty();

		SetCardGridLayoutParams();
		GenerateCards();
	}
	private void SetCardGridLayoutParams()
	{
		CardGridLayout cardGridLayout = this.GetComponent<CardGridLayout>();

		cardGridLayout.preferredPadding = gameDatas.preferredPaddingTopBottom;
		cardGridLayout.rows = gameDatas.rows;
		cardGridLayout.columns = gameDatas.columns;
		cardGridLayout.spacing = gameDatas.spacing;

		cardGridLayout.Invoke("CalculateLayoutInputHorizontal", 0.1f);
	}

	private void GetGameDatasByDifficulty()
	{
		Difficulty difficulty = (Difficulty)PlayerPrefs.GetInt("difficulty", (int)Difficulty.NORMAL);

		switch (difficulty)
		{
			case Difficulty.EASY:
				gameDatas = easyData;
				break;
			case Difficulty.NORMAL:
				gameDatas = normalData;
				break;
			case Difficulty.HARD:
				gameDatas = hardData;
				break;
		}
	}

	public void GenerateCards()
	{
		int cardCount = gameDatas.rows * gameDatas.columns;

		for(int i = 0; i < cardCount; i++)
		{
			GameObject card = Instantiate(cardPrefab, this.transform);
			card.transform.name = "Card (" + i.ToString() + ")";
		}
	}
}
