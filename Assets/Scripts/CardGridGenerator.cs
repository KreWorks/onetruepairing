using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardGridGenerator 
{
	GameObject cardPrefab;
	Color[] difficultyBackgrounds;
	Vector3 centerPosition;
	CardCollectionSO cardCollection;

	Vector2[] positions;
	float cardSize = 100;
	List<GameObject> cards;

	List<int> availableIndex;
	Difficulty difficulty;

	float headerHeightPercent = 0.09f;
	float paddingPercent = 0.2f;

	public CardGridGenerator(Vector3 position, GameObject cardPrefab, Color[] difficultyColors, CardCollectionSO cardCollection, Difficulty difficulty)
	{
		this.centerPosition = position;
		this.difficultyBackgrounds = difficultyColors;
		this.cardPrefab = cardPrefab;
		this.cardCollection = cardCollection;

		this.difficulty = difficulty;

		GenerateAvailableIndexes();
	}

	// Start is called before the first frame update
	public void Generate(Transform parent)
	{
		Vector2 playAreaSize = DifficultyHelper.GetPlayAreaSize(difficulty);
		int sizeX = (int)playAreaSize.x;
		int sizeY = (int)playAreaSize.y;
		
		int count = sizeX * sizeY;

		positions = new Vector2[count];
		cards = new List<GameObject>();

		GeneratePositions(sizeX, sizeY, this.centerPosition, parent);

		for (int i = 0; i < count; i += 2)
		{
			int randomIndex = GetRandomAvailableIndex();
			//The card
			CreateCard(positions[i], cardCollection.cards[randomIndex], parent);
			//The pair of the card
			CreateCard(positions[i + 1], cardCollection.cards[randomIndex+1], parent);
		}

		MixCards();
	}

	void GeneratePositions(int sizeX, int sizeY, Vector2 startPosition, Transform parent)
	{
		float camHeight = Camera.main.pixelHeight;
		//Camera.main.pixelWidth
		this.cardSize = sizeX == 4 ? camHeight * 0.23f : sizeX == 5 ? camHeight * 0.17f : camHeight * 0.14f;
		float padding = paddingPercent = sizeY;

		Vector2 startPadding = new Vector2(-(float)(sizeX - 1) / 2.0f * (cardSize + padding), -camHeight* headerHeightPercent - (float)(sizeY - 1) / 2.0f * (cardSize + padding));

		startPadding += new Vector2(parent.transform.position.x, parent.transform.position.y);
		for (int y = 0; y < sizeY; y++)
		{
			for (int x = 0; x < sizeX; x++)
			{
				int index = y * sizeX + x;

				positions[index] = startPadding + new Vector2((x) * (cardSize + padding), (y) * (cardSize + padding));
			}
		}
	}

	void MixCards()
	{
		for (int i = 0; i < cards.Count; i++)
		{
			int random = Random.Range(0, cards.Count - 1);
			while (random == i)
			{
				random = Random.Range(0, cards.Count - 1);
			}

			Vector3 newPosition = cards[i].transform.position;
			cards[i]. transform.position = cards[random].transform.position;
			cards[random].transform.position = newPosition;
		}
	}

	void GenerateAvailableIndexes()
	{
		availableIndex = new List<int>();
		int index = cardCollection.cards.Count;

		for(int i = 0; i < index; i++)
		{
			if (i % 2 == 0)
			{
				this.availableIndex.Add(i);
			}
		}
	}

	int GetRandomAvailableIndex()
	{
		int random = Random.Range(0, this.availableIndex.Count);
		int randomIndex = availableIndex[random];

		availableIndex.RemoveAt(random);

		return randomIndex;
	}

	void CreateCard(Vector2 position, CardSO cardSO, Transform parent)
	{
		//Create card object
		GameObject card = GameObject.Instantiate(cardPrefab, position, Quaternion.identity, parent);
		CardController cardController = card.GetComponent<CardController>();
		cardController.SetCardSO(cardSO);
		Sprite iconSprite = cardSO.GetCardImageBySize((int)difficulty);
		// Get the object spriteRenderer, and set the background
		SpriteRenderer bg = card.GetComponent<SpriteRenderer>();

		string bg_name = "Icons/bg" + DifficultyHelper.GetDifficultyString(difficulty) + "_" + DifficultyHelper.GetIconSizeByDifficulty(difficulty);
		Debug.Log(bg_name);
		Image[] icons = card.GetComponentsInChildren<Image>();
		Image icon = null;
		Image background = null;
		foreach(Image i in icons)
		{
			if(i.transform.name == "Icon")
			{
				icon = i;
			}
			else if(i.transform.name == "Background")
			{
				background = i;
			}
		}

		background.sprite = Resources.Load<Sprite>(bg_name);
		background.rectTransform.sizeDelta = new Vector2(cardSize, cardSize);

		icon.sprite = iconSprite;
		icon.rectTransform.sizeDelta = new Vector2(cardSize, cardSize);

		cards.Add(card);
	}

}
