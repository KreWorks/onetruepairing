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
		Debug.Log("Card was Clicked.");
		cardController.TransitionState(cardController.flippingState);
	}
}
