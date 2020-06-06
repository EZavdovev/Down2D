using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BackgroundEffects : MonoBehaviour
{
    AutoResetEvent resetEvent;
    public Transform light;
    public Transform light1;
    public float speedlight;
    public float speedlight1;
    Vector3 l;
    Vector3 l1;
    float x, y, z, x1, y1, z1;
    float ChangePos = 5.29f;
    Thread thread;
    bool IsStopped;
    bool IsEnd = false;
    void Start()
    {
        IsStopped = false;
        GameManager.PlayerFall.Subscribe(StopAndChangeRoad);
        l = light.position;
        l1 = light1.position;

        x = light.position.x;
        x1 = light1.position.x;

        y = light.position.y;
        y1 = light1.position.y;

        z = light.position.z;
        z1 = light1.position.z;
        
        thread = new Thread(CalculateRoadLights);
        thread.Start();
        resetEvent = new AutoResetEvent(false);
    }

    void StopAndChangeRoad(string type_stop)
    {
        if (type_stop == "Fall")
            IsStopped = true;
            
    }
    void CalculateRoadLights()
    {
        while (!IsEnd)
        {
            resetEvent.WaitOne();
            x += speedlight;
            l = new Vector3(x, y, z);
            if (x > 15 || x < 6)
                speedlight = -speedlight;
            x1 += speedlight1;
            l1 = new Vector3(x1, y1, z1);
            if (x1 > 5 || x1 < -4)
                speedlight1 = -speedlight1;
            if (IsStopped)
            {
                Thread.Sleep(1000);
                y -= ChangePos;
                y1 -= ChangePos;
                IsStopped = false;
            }
        }
    }
    void Update()
    {
        resetEvent.Set();
        light.position = l; 
        light1.position = l1;
    }

    private void OnApplicationQuit()
    {
        IsEnd = true;
        thread.Abort();
    }
    void OnDestroy()
    {
        IsEnd = true;
        thread.Abort();
    }
}
