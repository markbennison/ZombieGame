using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfGame : MonoBehaviour
{
    [SerializeField] private GameObject gameCompletePanel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameCompletePanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
