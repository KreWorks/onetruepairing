﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
	public CanvasManager canvasManager;

	public GameObject playArea;

	GameState gameState;
	public CardSelectionState cardSelectionState;
	public PairSelectionState pairSelectionState;
	public MemorizeCardsState memorizeCardsState;
	public MatchingCardsState matchingCardsState;
	//TODO EndGameState and MenuState

	public GameObject[] selectedCards;

	// Start is called before the first frame update
	void Start()
	{
		selectedCards = new GameObject[2];
		selectedCards[0] = null;
		selectedCards[1] = null;

		Difficulty difficulty = Difficulty.HARD; // (Difficulty) PlayerPrefs.GetInt("difficulty", (int) Difficulty.EASY);

		canvasManager.GenerateCardField(difficulty);

		InitStates();
	}

	void Update()
	{
		gameState.UpdateAction();
	}

	void InitStates()
	{
		cardSelectionState = new CardSelectionState(this);
		pairSelectionState = new PairSelectionState(this);
		memorizeCardsState = new MemorizeCardsState(this, 0.5f);
		matchingCardsState = new MatchingCardsState(this, 0.2f);

		gameState = cardSelectionState;
	}

	public void TransitionState(GameState newState)
	{
		Debug.Log(newState.ToString());
		gameState.EndState();
		gameState = newState;
		gameState.EnterState();
	}

	public void SetSelectedCard(GameObject selectedCard)
	{
		if (selectedCards[0] == null)
		{
			selectedCards[0] = selectedCard;
			TransitionState(pairSelectionState);
		}
		else if (selectedCards[1] == null)
		{
			selectedCards[1] = selectedCard;

			if (MatchSelectedCards())
			{
				TransitionState(matchingCardsState);
			}
			else
			{
				TransitionState(memorizeCardsState);
			}
		}
	}

	public void RemoveSelectedCards()
	{
		selectedCards[0] = null;
		selectedCards[1] = null;
	}

	bool MatchSelectedCards()
	{
		CardSO first = selectedCards[0].GetComponent<CardController>().cardType;
		CardSO second = selectedCards[1].GetComponent<CardController>().cardType;

		return first != null && second != null && first.cardName == second.pairName && first.pairName == second.cardName;
	}
}
