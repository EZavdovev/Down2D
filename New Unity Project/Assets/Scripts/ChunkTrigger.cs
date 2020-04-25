using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == GameManager.player.gameObject)
        {
            GameManager.PlayerFall.Publish("Fall");
        }
    }
}
