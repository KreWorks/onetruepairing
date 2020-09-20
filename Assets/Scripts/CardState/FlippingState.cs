using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlippingState : CardState
{
	public FlippingState(CardController cardController) : base(cardController)
	{
	}

	public override void UpdateActivity()
	{
		cardController.Flip();
	}
}
