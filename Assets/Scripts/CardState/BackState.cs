using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackState : CardState
{
	public BackState(CardController cardController) : base(cardController)
	{
	}

	public override void OnClickAction()
	{
		cardController.TransitionState(cardController.flippingState);
	}
}
