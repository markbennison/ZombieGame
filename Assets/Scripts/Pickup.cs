using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private float HealAmount = 25f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !collision.gameObject.GetComponent<HealthManager>().MaxHP)
        {
            collision.gameObject.GetComponent<HealthManager>().Heal(HealAmount);
            Destroy(gameObject);
        }
    }

}
