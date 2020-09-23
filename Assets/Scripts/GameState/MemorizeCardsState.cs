using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemorizeCardsState : GameState
{
	float timeToWait;
	float timer; 

	public MemorizeCardsState(GameManager gameManager, float timeToMemorize) : base(gameManager)
	{
		timeToWait = timeToMemorize;
	}

	public override void EnterState()
	{
		this.timer = 0.0f;
	}

	public override void UpdateAction()
	{
		timer += Time.deltaTime;

		if (timer >= timeToWait)
		{
			gameManager.TransitionState(gameManager.cardSelectionState);
			gameManager.RemoveSelectedCards();
		}
	}

	public override void EndState()
	{
		CardController first = gameManager.selectedCards[0].GetComponent<CardController>();
		first.TransitionState(first.backFlippingState);
		CardController second = gameManager.selectedCards[1].GetComponent<CardController>();
		second.TransitionState(second.backFlippingState);
	}
}
