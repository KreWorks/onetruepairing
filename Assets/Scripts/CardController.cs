using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour
{
	public Image background;
	public Image icon;

	public ParticleSystem _hearts;
	CardSO cardType;

	const int FORWARD = 1;
	const int BACKWARD = -1;
	public enum FlipState { NoFlipping, Flipping, BackFlipping };

	float flipSpeed = 400f;
	float angleTolerance = 10f;
	FlipState flipState = FlipState.NoFlipping;
	Color imageSideColor = new Color (43f / 256,43f / 256,43f/ 256);

	public CardManager cardManager;

	float waitBeforeBackFlip = 0.5f;
	float timerBeforeBackFlip = 0f;
	
    // Start is called before the first frame update
    void Start()
    {
        cardManager = (CardManager)FindObjectOfType(typeof(CardManager));
	}

	// Update is called once per frame
	void Update()
	{
		if (flipState == FlipState.Flipping)
		{
			Flip();
		}
		else if(flipState == FlipState.BackFlipping)
		{
			if (timerBeforeBackFlip < waitBeforeBackFlip)
			{
				timerBeforeBackFlip += Time.deltaTime;
			}
			else
			{
				BackFlip();
			}
		}
	}

	public void SetCardSO(CardSO card)
	{
		this.cardType = card;
	}

	void ChangeImages()
	{
		/*string spriteSize = PlayerPrefs.GetString("SpriteSize", "large");

		Color c = icon.GetComponent<SpriteRenderer>().color;
		icon.GetComponent<SpriteRenderer>().color = Color.white; // background.color;
		background.sprite = Resources.Load<Sprite>("Icons/background_" + spriteSize);
		background.color = imageSideColor;
		icon.SetActive(true);*/
	}

	void ChangeImagesBack()
	{
		/*string spriteSize = PlayerPrefs.GetString("SpriteSize", "large");
		string difficulty = PlayerPrefs.GetString("Difficulty", "Easy");

		icon.GetComponent<SpriteRenderer>().color = background.color;
		int colorIndex = difficulty == "Easy" ? 0 : difficulty == "Normal" ? 1 : 2;
		//background.color = cardManager.difficultyBackgrounds[colorIndex];
		string bg_name = "Icons/bg" + difficulty.ToLower() + "_" + spriteSize;
		background.sprite = Resources.Load<Sprite>(bg_name);
		background.color = Color.white;
		icon.SetActive(false);*/
	}
	
	void Flip()
	{/*
		float speed = 1 * flipSpeed * Time.deltaTime; 
		if (transform.rotation.eulerAngles.y >= 0f && transform.rotation.eulerAngles.y < 90f)
		{
			transform.RotateAround(transform.position, Vector3.up, speed);
		}
		else if (icon.activeInHierarchy == false && Mathf.Abs(transform.rotation.eulerAngles.y - 90f) < angleTolerance)
		{
			ChangeImages();
			transform.eulerAngles = new Vector3(transform.eulerAngles.x, 90f, transform.eulerAngles.z);
		} else if (transform.rotation.eulerAngles.y >= 90f && transform.rotation.eulerAngles.y < 180f)
		{
			transform.RotateAround(transform.position, Vector3.up, speed);
		} else if(icon.activeSelf == true && (transform.rotation.eulerAngles.y - 180f) < angleTolerance)
		{
			transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180f, transform.eulerAngles.z);
			cardManager.PlaySoundEffect("Sounds/flip");
			SetFlipState(FlipState.NoFlipping);
		}*/
	}

	void BackFlip()
	{
		/*
		float speed = -1 * flipSpeed * Time.deltaTime;
		if (transform.rotation.eulerAngles.y > 90f && transform.rotation.eulerAngles.y <= 180f)
		{
			transform.RotateAround(transform.position, Vector3.up, speed);
		}
		else if (icon.activeInHierarchy == true && Mathf.Abs(transform.rotation.eulerAngles.y - 90f) < angleTolerance)
		{
			transform.eulerAngles = new Vector3(transform.eulerAngles.x, 90f, transform.eulerAngles.z);
			ChangeImagesBack();
		}
		else if (transform.rotation.eulerAngles.y > 0f && transform.rotation.eulerAngles.y <= 90f)
		{
			transform.RotateAround(transform.position, Vector3.up, speed);
		}
		else if (icon.activeSelf == false && ((transform.rotation.eulerAngles.y) < angleTolerance || Mathf.Abs(transform.rotation.eulerAngles.y - 360f) < angleTolerance))
		{
			transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0f, transform.eulerAngles.z);
			cardManager.PlaySoundEffect("Sounds/backflip");
			SetFlipState(FlipState.NoFlipping);
			
		}*/
	}


	public void SetFlipState(FlipState fs)
	{
		if (fs == FlipState.BackFlipping)
		{
			timerBeforeBackFlip = 0f;
		}
		flipState = fs;
	}

	public FlipState GetFlipState()
	{
		return flipState;
	}

	private void OnDestroy()
	{
		Vector3 heartPos = new Vector3(transform.position.x, transform.position.y, -1.0f);
		ParticleSystem endGame = Instantiate(_hearts, heartPos, transform.rotation);
		endGame.Play();
	}
}
