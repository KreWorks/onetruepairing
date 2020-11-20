using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardController : MonoBehaviour, IPointerDownHandler
{
	public Image frontFace;
	public Image backface;
	
	public ParticleSystem _heartsParticleSystemPrefab;
	public CardSO cardType;

	GameManager gameManager; 

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

	float flipSpeed = 400f;
	float angleTolerance = 10f;
	Color imageSideColor = new Color(43f / 256, 43f / 256, 43f / 256);

	float waitBeforeBackFlip = 0.5f;
	float timerBeforeBackFlip = 0f;

	// Start is called before the first frame update
	void Start()
	{
		gameManager = (GameManager)FindObjectOfType(typeof(GameManager));

		frontState = new FrontState(this);
		backState = new BackState(this);
		flippingState = new FlippingState(this);
		backFlippingState = new BackFlippingState(this);
		hideAwayState = new HideAwayState(this, 1.00f);

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
		/*this.icon = icon;
		this.background = background;*/
	}

	public void TransitionState(CardState newState)
	{
		this.actualState.EndState();
		this.actualState = newState;
		this.actualState.EnterState();
	}

	public void SetBackfaceActive()
	{
		/*background.gameObject.SetActive(false);
		icon.gameObject.SetActive(false);*/
		backface.gameObject.SetActive(true);
	}

	public void SetFrontFaceActive()
	{
		/*background.gameObject.SetActive(true);
		icon.gameObject.SetActive(true);*/
		backface.gameObject.SetActive(false);
	}
	public void HideAllFaces()
	{
		/*background.gameObject.SetActive(false);
		icon.gameObject.SetActive(false);*/
		backface.gameObject.SetActive(false);
	}

	public void ChangeScale(float newScale)
	{
		backface.transform.localScale = new Vector3(newScale, 1, 1);
	/*	background.transform.localScale = new Vector3(newScale, 1, 1);
		icon.transform.localScale = new Vector3(newScale, 1, 1);*/
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
				gameManager.SetSelectedCard(this.gameObject);
			}
		}
	}
	
	public void BackFlip()
	{
		//Hide foreground
		if (backface.gameObject.activeSelf == false)
		{
			cardScale = cardScale * flipScale;
			ChangeScale(cardScale);
			//Show foreground
			if (flipTolerance > cardScale)
			{
				SetBackfaceActive();
			}
		}
		else
		{
			cardScale = cardScale * (1.0f / flipScale);
			ChangeScale(cardScale);

			if (cardScale >= 1.0f)
			{
				ChangeScale(1.0f);
				TransitionState(this.backState);
			}
		}
	}

	public void OnDestroy()
	{
		Debug.Log("Got destroyed");
		Vector3 heartPos = new Vector3(transform.position.x, transform.position.y, -1.0f);
		ParticleSystem endGame = Instantiate(_heartsParticleSystemPrefab, heartPos, transform.rotation);
		endGame.Play();
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		actualState.OnClickAction();
	}
}
