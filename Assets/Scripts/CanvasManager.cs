using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
	public GameObject cardPrefab;
	public CardCollectionSO cardCollection;

	public GameDatasSO easyData;
	public GameDatasSO normalData;
	public GameDatasSO hardData;

	GameDatasSO gameDatas;
	CardGridGenerator cardGridGenerator;

	List<CardController> cardControllers;

	void Awake()
	{
		cardControllers = new List<CardController>();
		GetGameDatasByDifficulty();

		cardGridGenerator = new CardGridGenerator(cardCollection, gameDatas);

		SetCardGridLayoutParams();
		GenerateCards();

		GameManager gameManager = (GameManager)FindObjectOfType(typeof(GameManager));
		gameManager.CardCount = gameDatas.rows * gameDatas.columns;
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
		Difficulty difficulty = (Difficulty)PlayerPrefs.GetInt("Difficulty", (int)Difficulty.NORMAL);

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

	private void GenerateCards()
	{
		int cardCount = gameDatas.rows * gameDatas.columns;

		for(int i = 0; i < cardCount; i++)
		{
			GameObject card = Instantiate(cardPrefab, this.transform);
			card.transform.name = "Card (" + i.ToString() + ")";

			cardControllers.Add(card.GetComponent<CardController>());
		}

		for(int i = 0; i < cardCount/ 2; i++)
		{
			CardSO randomCard = cardGridGenerator.GetRandomAvailableCardSO();
			SetRandomCardToGrid(randomCard);

			CardSO randomCardPair = cardGridGenerator.GetCardPairSO(randomCard.cardName);
			SetRandomCardToGrid(randomCardPair);
		}
	}

	private void SetRandomCardToGrid(CardSO randomCard)
	{
		int index = cardGridGenerator.GetRandomCardPositionIndex();
		CardController cardObject = cardControllers[index];
		cardObject.SetCardDatas(gameDatas.background, randomCard);
	}
}
