using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchingCardsState : GameState
{
	public MatchingCardsState(GameManager gameManager) : base(gameManager)
	{
	}

	public override void UpdateAction()
	{
		gameManager.BackFlipSelectedCards();
	}
}
