using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkTrigger : MonoBehaviour
{
    private GameObject player; 

    private void Awake() // Это один из способов. Мы ищем игрока через код. Лучше добавить игрока в инспекторе
    {
        player = GameObject.FindObjectOfType<Player>().gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            GameManager.PlayerFall.Publish("Fall");
        }
    }
    
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.PlayerFall.Publish("Fall");
        }
    }*/
}
