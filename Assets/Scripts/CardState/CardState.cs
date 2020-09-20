using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardState
{
	protected CardController cardController; 

	public CardState(CardController cardController)
	{
		this.cardController = cardController;
	}

	public virtual void EnterState() { return; }
	public virtual void EndState() { return; }
	public virtual void OnClickAction() { return; }
	public virtual void UpdateActivity() { return; }
	
}
