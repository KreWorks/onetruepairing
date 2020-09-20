using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemorizeState : CardState
{
	public float memorizeTime;
	public MemorizeState(CardController cardController, float memorizeTime) : base(cardController)
	{
		this.memorizeTime = memorizeTime;
	}

	public override void EnterState()
	{
		throw new System.NotImplementedException();
	}

	public override void EndState()
	{
		throw new System.NotImplementedException();
	}

	public override void OnClickAction()
	{
		throw new System.NotImplementedException();
	}

	public override void UpdateActivity()
	{
		throw new System.NotImplementedException();
	}
}
