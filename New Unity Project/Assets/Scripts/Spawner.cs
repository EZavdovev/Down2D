using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform SpawnPos;
    public GameObject Rocket;
    bool IsNextFloor;
    float Timer;

    void Start()
    {
        GameManager.PlayerFall.Subscribe(NextF);
        Timer = 3.0f;
        IsNextFloor = false;
        StartCoroutine(SpawnObj());
    }

    void Repeat()
    {
        StartCoroutine(SpawnObj());
    }

    public void NextF(string info)
    {
        if(info == "Fall")
            IsNextFloor = true;
    }

    IEnumerator SpawnObj()
    {
        yield return new WaitForSeconds(Timer);
        if (IsNextFloor == true)
        {
            yield return new WaitForSeconds(4.0f);
            Timer -= 0.05f;
            IsNextFloor = false;
        }
        float pos = Random.Range(-8.5f, 8.5f);
        SpawnPos.position += new Vector3(pos, 0);
        PoolManager.getGameObjectFromPool(Rocket, SpawnPos);
        SpawnPos.position -= new Vector3(pos, 0);
        Repeat();
    }
}
