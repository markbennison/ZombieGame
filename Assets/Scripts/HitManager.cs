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
        if (hitPoints <= 0)
        {
            Invoke("SelfTerminate", 0f);
        }
    }

    void SelfTerminate()
    {
        GameObject GetSManager = GameObject.FindWithTag("UIManager").gameObject;
        if (GetSManager != null)
        {
            GetSManager.TryGetComponent<UIManager>(out UIManager manager);
            manager.UpdateScore();
        }
        Spawner.NumberSpawned--;
		Destroy(gameObject);
    }
}