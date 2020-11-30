using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class CardController : MonoBehaviour, IPointerDownHandler
{
	public Image frontFace;
	public Image backFace;

	public CardSO cardType;

	GameManager gameManager;
	HeartsManager heartsManager;
	public AudioManager audioManager;

	public CardState actualState;
	public FrontState frontState;
	public BackState backState;
	public FlippingState flippingState;
	public BackFlippingState backFlippingState;
	public MemorizeState memorizeState;
	public HideAwayState hideAwayState;

	float cardScale = 1.0f;
	float flipSpeed = 2.0f;
	float flipTolerance = 0.05f;

	// Start is called before the first frame update
	void Start()
	{
		gameManager = (GameManager)FindObjectOfType(typeof(GameManager));
		heartsManager = (HeartsManager)FindObjectOfType(typeof(HeartsManager));
		audioManager = (AudioManager)FindObjectOfType(typeof(AudioManager));

		frontState = new FrontState(this);
		backState = new BackState(this);
		flippingState = new FlippingState(this);
		backFlippingState = new BackFlippingState(this);
		hideAwayState = new HideAwayState(this);

		actualState = backState;
	}

	// Update is called once per frame
	void Update()
	{
		actualState.UpdateActivity();
	}

	internal void SetCardDatas(Sprite background, CardSO card)
	{
		this.cardType = card;

		frontFace.sprite = card.cardImage;
		backFace.sprite = background;

		backFace.gameObject.SetActive(true);
		frontFace.gameObject.SetActive(false);
	}

	public void TransitionState(CardState newState)
	{
		this.actualState.EndState();
		this.actualState = newState;
		this.actualState.EnterState();
	}

	public void SwitchFaces()
	{
		backFace.gameObject.SetActive(!backFace.gameObject.activeSelf);
		frontFace.gameObject.SetActive(!frontFace.gameObject.activeSelf);
	}

	public void InactivateCard()
	{
		backFace.gameObject.SetActive(false);
		frontFace.gameObject.SetActive(false);

		Image cardImage = this.GetComponent<Image>();
		Color newColor = cardImage.color;
		newColor.a = 0.0f;
		cardImage.color = newColor;
	}

	public void AddHeartSpawner()
	{
		heartsManager.SpawnHeartSpawner(this.transform.position);
	}

	public void ChangeScale(float newScale)
	{
		this.transform.localScale = new Vector3(newScale, 1, 1);
	}

	public void Flip()
	{
		//Hide background
		if (backFace.gameObject.activeSelf == true)
		{
			cardScale = cardScale - (flipSpeed * Time.deltaTime);
			ChangeScale(cardScale);
			//Show foreground
			if (flipTolerance > cardScale)
			{
				SwitchFaces();
			}
		}
		else
		{
			cardScale = cardScale  + (flipSpeed * Time.deltaTime);
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
		if (backFace.gameObject.activeSelf == false)
		{
			cardScale = cardScale - (flipSpeed * Time.deltaTime);
			ChangeScale(cardScale);
			//Show foreground
			if (flipTolerance > cardScale)
			{
				SwitchFaces();
			}
		}
		else
		{
			cardScale = cardScale + (flipSpeed * Time.deltaTime);
			ChangeScale(cardScale);

			if (cardScale >= 1.0f)
			{
				ChangeScale(1.0f);
				TransitionState(this.backState);
			}
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		actualState.OnClickAction();
	}
}
