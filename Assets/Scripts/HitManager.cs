using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitManager : MonoBehaviour
{
    [SerializeField]
    float hitPoints = 15;

    void Hit(float rawDamage)
    {
        hitPoints -= rawDamage;

		//AudioManager.Instance.PlayAtPoint("EnemyGrunt");
		if (hitPoints <= 0)
        {
            Invoke("SelfTerminate", 0f);
        }
    }

    void SelfTerminate()
    {
        Spawner.NumberSpawned--;
        GameManager.IncrementScore(1);
		Destroy(gameObject);
    }
}