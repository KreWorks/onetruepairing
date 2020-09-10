using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardState
{
	public abstract void EnterState();
	public abstract void ExitState();
	public abstract void OnClickAction();
	public abstract void UpdateActivity();
	
}
