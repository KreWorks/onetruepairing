using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartBummController : MonoBehaviour
{
	public GameObject heartPrefab;
	public int heartCount;
	public Gradient colors;

	public int minSpeed = 3000;
	public int maxSpeed = 6000;

	public float timeToLive = 3.0f;
	List<GameObject> hearts;

	float timer;

	void Start()
    {
		hearts = new List<GameObject>();

		timer = 0.0f;

        for(int i = 0; i < heartCount; i++)
		{
			GameObject heart = Instantiate(heartPrefab, this.transform);
			float random = (float)Random.Range(0, 100) / 100.0f;
			heart.GetComponent<Image>().color = colors.Evaluate(random);

			int randomRotation = Random.Range(0, 360);
			heart.transform.Rotate(new Vector3(0, 0, randomRotation));

			Rigidbody2D rb = heart.GetComponent<Rigidbody2D>();
			float speed = Random.Range(minSpeed, maxSpeed);
			rb.AddForce(heart.transform.right * speed, ForceMode2D.Impulse);

			hearts.Add(heart);
		}
    }

    // Update is called once per frame
    void Update()
    {
		timer += Time.deltaTime;

		if (timer >= timeToLive)
		{
			while(hearts.Count > 0)
			{
				GameObject heart = hearts[0];
				hearts.Remove(heart);
				Destroy(heart);
			}

			Destroy(this.gameObject);
		}
    }
}
