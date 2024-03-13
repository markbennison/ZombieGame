using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject objectToSpawn;
	[SerializeField] float secondsBetweenSpawns = 5f;

    float timer = 0f;

    static int maximumSpawns = 40;
    public static int NumberSpawned { get;  set; }

    Quaternion spawnRotation;

	void Start()
    {
        Reset();

		bool rotate = GetComponent<SpriteRenderer>().flipX;
		GetComponent<SpriteRenderer>().enabled = false;
        Spawn();
	}

    void Update()
    {
        timer += Time.deltaTime;
        if(timer > secondsBetweenSpawns)
        {
            Spawn();
			timer = 0f;
		}
	}

	public static void Reset()
	{
        NumberSpawned = 0;
	}

	void Spawn()
    {
        if (NumberSpawned < maximumSpawns)
        {
            Instantiate<GameObject>(objectToSpawn, gameObject.transform.position, gameObject.transform.rotation);
			NumberSpawned++;

		}
	}
}
