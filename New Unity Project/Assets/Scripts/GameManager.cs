using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static EnemyDieEvent EnemyDied = new EnemyDieEvent();
    public static AdventureEvent PlayerFall = new AdventureEvent();
    public static EventCleaner EventClean = new EventCleaner();
    public Transform Pool;
    public static Player player;
    private void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        PoolManager.init(Pool);
    }
}
