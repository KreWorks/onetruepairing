using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState 
{
	protected GameManager gameManager;

    public GameState(GameManager gameManager)
	{
		this.gameManager = gameManager;
	}
}
