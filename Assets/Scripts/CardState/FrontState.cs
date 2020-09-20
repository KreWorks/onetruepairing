using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontState : CardState
{
	public FrontState(CardController cardController) : base(cardController)
	{
	}

	public override void OnClickAction()
	{
		cardController.TransitionState(cardController.flippingState);
	}
}
