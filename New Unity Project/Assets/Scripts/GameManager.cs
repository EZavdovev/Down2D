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

    private void Start()
    {
        PoolManager.init(Pool);
    }


}
