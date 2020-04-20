using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCleaner : MonoBehaviour
{
   
    public void Clear()
    {
        GameManager.PlayerFall.Clear();
        GameManager.EnemyDied.Clear();
    }
}
