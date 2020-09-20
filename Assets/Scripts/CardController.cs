using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardController : MonoBehaviour, IPointerDownHandler
{
	public Image background;
	public Image icon;
	public Image backface;

	public ParticleSystem _hearts;
	CardSO cardType;

	public CardState actualState;
	public FrontState frontState;
	public BackState backState;
	public FlippingState flippingState;
	public BackFlippingState backFlippingState;
	public MemorizeState memorizeState;
	public HideAwayState hideAwayState;

	float cardScale = 1.0f;
	float flipScale = 0.95f;
	float flipTolerance = 0.15f;

	const int FORWARD = 1;
	const int BACKWARD = -1;

	float flipSpeed = 400f;
	float angleTolerance = 10f;
	Color imageSideColor = new Color(43f / 256, 43f / 256, 43f / 256);

	public CardManager cardManager;

	float waitBeforeBackFlip = 0.5f;
	float timerBeforeBackFlip = 0f;

	// Start is called before the first frame update
	void Start()
	{
		cardManager = (CardManager)FindObjectOfType(typeof(CardManager));

		frontState = new FrontState(this);
		backState = new BackState(this);
		flippingState = new FlippingState(this);
		backFlippingState = new BackFlippingState(this);
		memorizeState = new MemorizeState(this, 5.0f);
		hideAwayState = new HideAwayState(this);

		actualState = backState;
	}

	// Update is called once per frame
	void Update()
	{
		actualState.UpdateActivity();
	}

	public void SetCardSO(CardSO card)
	{
		this.cardType = card;
	}

	public void SetImages(Image backface, Image icon, Image background)
	{
		this.backface = backface;
		this.icon = icon;
		this.background = background;
	}

	public void TransitionState(CardState newState)
	{
		this.actualState.EndState();
		this.actualState = newState;
		this.actualState.EnterState();
	}

	public void SetBackfaceActive()
	{
		background.gameObject.SetActive(false);
		icon.gameObject.SetActive(false);
		backface.gameObject.SetActive(true);
	}

	public void SetFrontFaceActive()
	{
		background.gameObject.SetActive(true);
		icon.gameObject.SetActive(true);
		backface.gameObject.SetActive(false);
	}

	public void ChangeScale(float newScale)
	{
		backface.transform.localScale = new Vector3(newScale, 1, 1);
		background.transform.localScale = new Vector3(newScale, 1, 1);
		icon.transform.localScale = new Vector3(newScale, 1, 1);
	}

	public void Flip()
	{
		//Hide background
		if (backface.gameObject.activeSelf == true)
		{
			cardScale = cardScale * flipScale;
			ChangeScale(cardScale);
			//Show foreground
			if (flipTolerance > cardScale)
			{
				SetFrontFaceActive();
			}
		}
		else
		{
			cardScale = cardScale * (1.0f / flipScale);
			ChangeScale(cardScale);

			if(cardScale >= 1.0f)
			{
				ChangeScale(1.0f);
				TransitionState(this.frontState);
			}
		}
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


	private void OnDestroy()
	{
		Vector3 heartPos = new Vector3(transform.position.x, transform.position.y, -1.0f);
		ParticleSystem endGame = Instantiate(_hearts, heartPos, transform.rotation);
		endGame.Play();
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		actualState.OnClickAction();
	}
}
