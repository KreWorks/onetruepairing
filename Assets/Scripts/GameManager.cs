using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	CardController[] selectedCards;

	// Start is called before the first frame update
	void Start()
	{
		selectedCards = new CardController[2];
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
		memorizeCardsState = new MemorizeCardsState(this, 1.0f);
		matchingCardsState = new MatchingCardsState(this);

		gameState = cardSelectionState;
	}

	public void TransitionState(GameState newState)
	{
		gameState.EnterState();
		gameState = newState;
		gameState.EndState();
	}

	public void SetSelectedCard(CardController selectedCard)
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

	public void RemoveSelectedCard(CardController selectedCard)
	{
		if (selectedCards[0] == selectedCard)
		{
			selectedCards[0] = null;
		}
		else if (selectedCards[1] == selectedCard)
		{
			selectedCards[1] = null;
		}
	}

	bool MatchSelectedCards()
	{
		return selectedCards[0] != null && selectedCards[1] != null && selectedCards[0].cardType.cardName == selectedCards[1].cardType.pairName && selectedCards[0].cardType.pairName == selectedCards[1].cardType.cardName;
	}

	public void BackFlipSelectedCards()
	{
		foreach(CardController card in selectedCards)
		{
			card.TransitionState(card.backFlippingState);
			RemoveSelectedCard(card);
		}
	}

	public void HideAwayMatchingCards()
	{
		foreach(CardController card in selectedCards)
		{
			card.TransitionState(card.hideAwayState);
		}
	}
}
