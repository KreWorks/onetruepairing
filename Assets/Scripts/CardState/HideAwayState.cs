using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAwayState : CardState
{
	float timer;
	float timeToWaitBeforeDestroy;
	public HideAwayState(CardController cardController, float timeToWait) : base(cardController)
	{
		timeToWaitBeforeDestroy = timeToWait;
	}

	public override void EnterState()
	{
		GameObject.Destroy(cardController.gameObject);
	}

}
