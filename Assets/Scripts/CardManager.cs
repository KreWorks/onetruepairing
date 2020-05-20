using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CardManager : MonoBehaviour
{
	public GameObject _card;

	public Color[] difficultyBackgrounds;

	public AudioSource audioSource;

	string[] icons;
	string[] iconPairs;
	Vector2[] positions;
	List<GameObject> cards;

	GameObject card1 = null;
	GameObject card2 = null;

	CardController.FlipState flipState;
	
    // Start is called before the first frame update
    public void StartGame()
    {
		flipState = CardController.FlipState.NoFlipping;

		icons = new string[] {  "avocado",  "pi", "snowman", "pig", "cane", "tapemachine", "baby", "orange", "pear", "cloud", "tophat", "banana", "mountain", "cheese", "ribbon"};
		iconPairs = new string[] { "egg", "pie", "eight", "socket", "candycane", "snail", "pineapple", "ball", "bulb", "sheep", "pot", "boomerang", "piramis", "cake", "candy"};

		int sizeX = PlayerPrefs.GetInt("sizeX", 4);
		int sizeY = PlayerPrefs.GetInt("sizeY", 3);
		int count = sizeX * sizeY;
		positions = new Vector2[count];

		cards = new List<GameObject>();

		string spriteSize = PlayerPrefs.GetString("SpriteSize", "large");

		GeneratePositions(sizeX, sizeY, transform.position);

		//Debug.Log("Size of points: " + positions.Length);
		List<int> availableIndex = SetAvailableIndexes(icons.Length);

		for (int i = 0; i < count; i+=2)
		{
			//int random = Random.Range(0, icons.Length - 1);
			int randomIndex = Random.Range(0, availableIndex.Count - 1);
			int random = availableIndex[randomIndex];
			if (availableIndex.Exists(x => x == random))
			{
				availableIndex.Remove(random);
				//The card
				CreateCube(positions[i], icons[random], spriteSize, i);
				//The pair of the card
				CreateCube(positions[i + 1], iconPairs[random], spriteSize, i);
			}

			if (availableIndex.Count == 0)
			{
				availableIndex = SetAvailableIndexes(icons.Length);
			}
		}

		MixCards();
	}

	List<int> SetAvailableIndexes(int count)
	{
		List<int> availableIndex = new List<int>();

		for (int i = 0; i < count; i++)
		{
			availableIndex.Add(i);
		}

		return availableIndex;
	}

	// Update is called once per frame
	void Update()
    {
		//Anything happens only if nothing is flipping
		if (flipState == CardController.FlipState.NoFlipping)
		{
			if (Input.GetMouseButtonDown(0))
			{
				MouseHit();
			}
			if (card1 != null && card2 != null)
			{
				Pairing();
			}
		}

		int cardCounter = FindObjectsOfType<CardController>().Length; 
		if(cardCounter == 0)
		{
			//TODO timer reference
			int point = 100; //  CalculatePoints((int)timerCount);

			PlayerPrefs.SetInt("ActualPoints", point);

			ParticleSystem[] bombs = FindObjectsOfType<ParticleSystem>();
			foreach(ParticleSystem ps in bombs)
			{
				Destroy(ps);
			}
			//SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}
	}

	int CalculatePoints(int time)
	{
		string difficulty = PlayerPrefs.GetString("Difficulty", "Easy");
		int limit = difficulty == "Easy" ? 300 : difficulty == "Normal" ? 450 : 600;

		int point = limit - time;

		point = point < 0 ? 0 : point;

		return point;
	}

	void MouseHit()
	{
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

		RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

		if (hit.collider != null && hit.collider.gameObject.tag == "Card")
		{
			CardController hittedCard = hit.collider.gameObject.GetComponent<CardController>();
			hittedCard.SetFlipState(CardController.FlipState.Flipping);
			if (card1 == null)
			{
				card1 = hit.collider.gameObject;
			}
			else if (card2 == null)
			{
				card2 = hit.collider.gameObject;
			}
		}
	}

	void Pairing()
	{
		string icon1 = card1.transform.GetChild(card1.transform.childCount - 1).gameObject.GetComponent<SpriteRenderer>().sprite.name;
		string icon2 = card2.transform.GetChild(card2.transform.childCount - 1).gameObject.GetComponent<SpriteRenderer>().sprite.name;

		if (IsPair(icon1, icon2) && card2.GetComponent<CardController>().GetFlipState() == CardController.FlipState.NoFlipping)
		{
			Destroy(card1);
			Destroy(card2);
			audioSource.PlayOneShot(Resources.Load<AudioClip>("Sounds/cupp2"));
			card1 = null;
			card2 = null;
		}
		else if (!IsPair(icon1, icon2) && card2.GetComponent<CardController>().GetFlipState() == CardController.FlipState.NoFlipping)
		{
			card1.GetComponent<CardController>().SetFlipState(CardController.FlipState.BackFlipping);
			card2.GetComponent<CardController>().SetFlipState(CardController.FlipState.BackFlipping);
			card1 = null;
			card2 = null;
		}
	}



	bool IsPair(string icon1, string icon2)
	{
		icon1 = RemoveSize(icon1);
		icon2 = RemoveSize(icon2);

		for (int i = 0; i < icons.Length; i++)
		{
			if ((icon1 == icons[i] && icon2 == iconPairs[i]) || (icon2 == icons[i] && icon1 == iconPairs[i]))
			{
				return true;
			}
		}

		return false;
	}

	string RemoveSize(string iconName)
	{
		iconName = iconName.Replace("_large", "");
		iconName = iconName.Replace("_normal", "");
		iconName = iconName.Replace("_small", "");

		return iconName;
	}

	void CreateCube(Vector2 position, string iconName, string spriteSize, int index)
	{
		string difficulty = PlayerPrefs.GetString("Difficulty", "Easy");
		//Create card object
		GameObject card = Instantiate(_card, position, transform.rotation);
		// Get the object spriteRenderer, and set the background
		SpriteRenderer bg = card.GetComponent<SpriteRenderer>();
		//string bg_name = "Icons/background_" + spriteSize;
		string bg_name = "Icons/bg" + difficulty.ToLower() + "_" + spriteSize;
		bg.sprite = Resources.Load<Sprite>(bg_name);

		//Background color depends on the difficulty level
		int colorIndex = difficulty == "Easy" ? 0 : difficulty == "Normal" ? 1 : 2;
		//bg.color = difficultyBackgrounds[colorIndex];
		bg.color = Color.white;

		//Get the icon SpriteRenderer and set an item
		GameObject iconObj = card.transform.GetChild(card.transform.childCount - 1).gameObject;
		SpriteRenderer icon = iconObj.GetComponent<SpriteRenderer>();

		string icon_name = iconName + "_" + spriteSize;
		icon.sprite = Resources.Load<Sprite>("Icons/" + icon_name);

		cards.Add(card);
	}

	void GeneratePositions(int sizeX, int sizeY, Vector2 startPosition)
	{
		float shiftStep = sizeX == 4 ? 3f : sizeX == 5 ? 2.5f : 2f;
		Vector2 shiftStart = sizeX == 4 ? new Vector2(-4.5f, 3f) : sizeX == 5 ? new Vector2(-5f, 3.75f) : new Vector2(-5f, 4f);

		int count = sizeX * sizeY;

		for (int i = 0; i < count; i++)
		{
			float shiftX = shiftStart.x + Mathf.Floor(i / sizeY) * shiftStep;
			float shiftY = shiftStart.y - (i % sizeY) * shiftStep;
			Vector3 shift = new Vector3(shiftX, shiftY, 0);

			//Create card object
			positions[i] = transform.position + shift;
		}
	}

	void MixCards()
	{
		for(int i = 0; i < cards.Count; i++)
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

	public void PlaySoundEffect(string audioName)
	{
		audioSource.PlayOneShot(Resources.Load<AudioClip>(audioName));
	}
}
