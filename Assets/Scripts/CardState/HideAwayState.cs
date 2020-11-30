using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAwayState : CardState
{
	public HideAwayState(CardController cardController) : base(cardController)
	{
	}

	public override void EnterState()
	{
		cardController.InactivateCard();
		cardController.AddHeartSpawner();
		cardController.audioManager.Play("Cupp");
	}

}
