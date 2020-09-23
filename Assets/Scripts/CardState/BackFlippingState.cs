using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackFlippingState : CardState
{
	public BackFlippingState(CardController cardController) : base(cardController)
	{
	}

	public override void UpdateActivity()
	{
		cardController.BackFlip();
	}
}
