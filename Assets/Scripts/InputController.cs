using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetMouseButtonDown(0))
		{
			Debug.Log("Kattintást érzékeli");
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

			RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);


			if (hit.collider != null && hit.collider.gameObject.tag == "Card")
			{
				CardController hittedCard = hit.collider.gameObject.GetComponent<CardController>();
				hittedCard.actualState.OnClickAction();
				Debug.Log("Idejön");
				//hittedCard.SetFlipState(CardController.FlipState.Flipping);
				/*
				 * TODO some observer with card objects
				if (card1 == null)
				{
					card1 = hit.collider.gameObject;
				}
				else if (card2 == null)
				{
					card2 = hit.collider.gameObject;
				}
				*/
			}
		}
	}
}
