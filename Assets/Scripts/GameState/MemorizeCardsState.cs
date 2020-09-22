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
			gameManager.BackFlipSelectedCards();
			gameManager.TransitionState(gameManager.cardSelectionState);
		}
	}
}
