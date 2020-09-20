using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardGridGenerator 
{
	GameObject cardPrefab;
	Color[] difficultyBackgrounds;
	CardCollectionSO cardCollection;

	Vector2[] positions;
	float cardSize;
	List<GameObject> cards;

	List<int> availableIndex;
	Difficulty difficulty;

	float headerHeight = 100.0f;
	float padding = 1.0f;

	public CardGridGenerator(GameObject cardPrefab, Color[] difficultyColors, CardCollectionSO cardCollection, Difficulty difficulty)
	{
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

		this.positions = new Vector2[count];
		cards = new List<GameObject>();

		GeneratePositions(sizeX, sizeY, parent);
		for (int i = 0; i < count; i += 2)
		{
			int randomIndex = GetRandomAvailableIndex();
			//The card
			CreateCard(this.positions[i], cardCollection.cards[randomIndex], parent);
			//The pair of the card
			CreateCard(this.positions[i + 1], cardCollection.cards[randomIndex+1], parent);
		}

		MixCards();
	}

	void GeneratePositions(int sizeX, int sizeY, Transform parent)
	{
		float camHeight = Camera.main.pixelHeight;

		float headerHeightPercent = this.headerHeight / camHeight;
		float paddingPercent = (this.padding * (sizeY + 1)) / camHeight;

		this.cardSize = (camHeight) / sizeY;

		float startPaddingX = -(float)(cardSize * (sizeX - 1) / 2.0f) - (float)(this.padding * (sizeX ) / 2.0f);
		float startPaddingY = -(float)(cardSize * (sizeY - 1) / 2.0f) - (float)(this.padding * (sizeY ) / 2.0f) - headerHeight / 2.0f;

		Vector2 startPadding = new Vector2(startPaddingX, startPaddingY);

		startPadding += new Vector2(parent.transform.position.x, parent.transform.position.y);
		for (int y = 0; y < sizeY; y++)
		{
			for (int x = 0; x < sizeX; x++)
			{
				int index = y * sizeX + x;

				this.positions[index] = startPadding + new Vector2((x / 2.0f) * cardSize + (2 * x * this.padding), (y / 2.0f) * cardSize + (2 * y * this.padding));
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
			cards[i].transform.position = cards[random].transform.position;
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

		Image[] icons = card.GetComponentsInChildren<Image>();
		Image icon = null;
		Image background = null;
		Image backFace = null;
		foreach(Image i in icons)
		{
			if(i.transform.name == "Icon")
			{
				icon = i;
			}
			else if(i.transform.name == "Backface")
			{
				backFace  = i;
			}
			else if(i.transform.name == "Background")
			{
				background = i;
			}
		}

		string bf_name = "Icons/bg" + DifficultyHelper.GetDifficultyString(difficulty) + "_" + DifficultyHelper.GetIconSizeByDifficulty(difficulty);
		backFace.sprite = Resources.Load<Sprite>(bf_name);
		backFace.rectTransform.sizeDelta = new Vector2(cardSize, cardSize);

		string bg_name = "Icons/background" + "_" + DifficultyHelper.GetIconSizeByDifficulty(difficulty);
		background.sprite = Resources.Load<Sprite>(bg_name);
		background.rectTransform.sizeDelta = new Vector2(cardSize, cardSize);

		icon.sprite = cardSO.GetCardImageBySize((int)difficulty);
		icon.rectTransform.sizeDelta = new Vector2(cardSize, cardSize);

		cardController.SetImages(backFace, icon, background);
		cardController.SetBackfaceActive();

		cards.Add(card);
	}

}
