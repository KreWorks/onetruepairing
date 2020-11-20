using UnityEngine;

[CreateAssetMenu(fileName = "GameDatas", menuName = "Game/Datas")]
public class GameDatasSO : ScriptableObject
{
	[Header("Difficulty Game Settings")]
	//[Tooltip("Arbitary text message")]
	public Difficulty difficulty;
	public int rows;
	public int columns;

	[Header("Card Background")]
	public Sprite background;

	[Header("Griid Layout variables")]
	public int preferredPaddingTopBottom;
	public Vector2 spacing;
}
