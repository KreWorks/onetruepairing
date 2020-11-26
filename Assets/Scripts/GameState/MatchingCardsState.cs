using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchingCardsState : GameState
{
	float timeToWait;
	float timer;

	public MatchingCardsState(GameManager gameManager, float timeToWait) : base(gameManager)
	{
		this.timeToWait = timeToWait;
	}

	public override void EnterState()
	{
		timer = 0.0f;
	}

	public override void UpdateAction()
	{
		timer += Time.deltaTime;

		if (timer >= timeToWait)
		{
			CardController first = gameManager.selectedCards[0].GetComponent<CardController>();
			first.TransitionState(first.hideAwayState);

			CardController second = gameManager.selectedCards[1].GetComponent<CardController>();
			second.TransitionState(second.hideAwayState);

			gameManager.TransitionState(gameManager.cardSelectionState);
		}
	}

	public override void EndState()
	{
		gameManager.RemoveSelectedCards();
		gameManager.CardCount -= 2;
	}

}
