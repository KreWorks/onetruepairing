using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
	public float sidebarPercent;
	public RectTransform sideBar;
	public RectTransform playArea;

	public GameObject cardPrefab;
	public CardCollectionSO cardCollection;

	CanvasScaler canvasScaler;
	Vector2 canvasResolution;
	float canvasWidth;

	void Awake()
	{
		canvasScaler = this.GetComponent<CanvasScaler>();
		Debug.Log(canvasScaler);
		canvasResolution = canvasScaler.referenceResolution;

		canvasWidth = canvasResolution.x;
		StartResizer();

	}

	// Start is called before the first frame update
	void Start()
    {
		
    }

	public void StartResizer()
	{
		float sidebarWidth = canvasWidth * sidebarPercent;
		float playAreaWidth = canvasWidth - sidebarWidth;

		sideBar.sizeDelta = new Vector2(sidebarWidth, 0);
		playArea.sizeDelta = new Vector2(playAreaWidth, 0);
	}

	public void GenerateCardField(Difficulty difficulty)
	{
		Debug.Log(canvasResolution);
		CardGridGenerator cardGenenrator = new CardGridGenerator(cardPrefab, cardCollection, difficulty, canvasResolution.y, canvasWidth * (1 -  sidebarPercent));

		cardGenenrator.Generate(playArea.transform);
	}
}
