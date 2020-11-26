using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartsManager: MonoBehaviour
{
	public GameObject heartSpawnerPrefab;

	public void SpawnHeartSpawner(Vector3 position)
	{
		GameObject heartSpawner = Instantiate(heartSpawnerPrefab, position, Quaternion.identity, this.transform);
	}
}
