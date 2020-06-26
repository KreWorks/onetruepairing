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
		int sizeX = PlayerPrefs.GetInt("sizeX", 4);
		int sizeY = PlayerPrefs.GetInt("sizeY", 3);
		
		int count = sizeX * sizeY;

		positions = new Vector2[count];
		cards = new List<GameObject>();

		GeneratePositions(sizeX, sizeY, this.centerPosition, parent);

		for (int i = 0; i < count; i += 2)
		{
			int randomIndex = GetRandomAvailableIndex();
			//The card
			CreateCard(positions[i], cardCollection.cards[randomIndex].GetCardImageBySize((int)difficulty), parent);
			//The pair of the card
			CreateCard(positions[i + 1], cardCollection.cards[randomIndex+1].GetCardImageBySize((int)difficulty), parent);
			
		}

		MixCards();
	}

	void GeneratePositions(int sizeX, int sizeY, Vector2 startPosition, Transform parent)
	{

		//Camera.main.pixelWidth
		this.cardSize = sizeX == 4 ? Screen.height * 0.30f : sizeX == 5 ? Screen.height * 0.25f : Screen.height * 0.20f;
		float padding = -20.0f;

		Vector2 startPadding = new Vector2(-(sizeX - 1) / 2 * (cardSize + padding), -(sizeY - 1) / 2 * (cardSize + padding));

		startPadding = new Vector2(parent.transform.position.x, parent.transform.position.y);
		for (int y = 0; y < sizeY; y++)
		{
			for (int x = 0; x < sizeX; x++)
			{
				int index = y * sizeX + x;

				positions[index] = startPadding + new Vector2((x - (sizeX - 1) / 2.0f) * (cardSize + padding), (y - (sizeY - 1) / 2.0f) * (cardSize + padding));
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

	void CreateCard(Vector2 position, Sprite iconSprite, Transform parent)
	{
		//Create card object
		GameObject card = GameObject.Instantiate(cardPrefab, position, Quaternion.identity, parent);
		// Get the object spriteRenderer, and set the background
		SpriteRenderer bg = card.GetComponent<SpriteRenderer>();

		string bg_name = "Icons/bg" + DifficultyHelper.GetDifficultyString(difficulty) + "_" + DifficultyHelper.GetIconSizeByDifficulty(difficulty);

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
		//Background color depends on the difficulty level
		background.color = difficultyBackgrounds[(int)this.difficulty];
		background.rectTransform.sizeDelta = new Vector2(cardSize, cardSize);

		icon.sprite = iconSprite;
		icon.rectTransform.sizeDelta = new Vector2(cardSize, cardSize);

		cards.Add(card);
	}

}
