using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDieEvent : MonoBehaviour
{
    public List<Action<string>> _callbacks = new List<Action<string>>();
    
    public void Subscribe(Action<string> callback)
    {
        _callbacks.Add(callback);
    }

    public void Publish(string enemy)
    {
        foreach(Action<string> callback in _callbacks)
        {
            callback(enemy);
        }
    }

    public void Clear()
    {
        _callbacks.Clear();
    }
}
