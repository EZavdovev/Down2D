using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AdventureEvent : MonoBehaviour
{
    public List<Action<string>> _callbacks = new List<Action<string>>();

    public void Subscribe(Action<string> callback)
    {
        _callbacks.Add(callback);
    }

    public void Publish(string fall)
    {
        foreach (Action<string> callback in _callbacks)
        {
            callback(fall);
        }
    }

    public void Clear()
    {
      _callbacks.Clear();
    }
}
