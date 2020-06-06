using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform SpawnPos;
    public GameObject Enemy;
    float Timer;
    bool IsNextFloor;
    const float RightPos = 19.0f;
    void Start()
    {
        GameManager.PlayerFall.Subscribe(NextF);
        Timer = 1.00f;
        IsNextFloor = false;
       // SpawnPos.position += new Vector3(0, 0, 9.584f);
        StartCoroutine(SpawnObj());
    }

    public void NextF(string info)
    {
        if(info == "Fall")
            IsNextFloor = true;
    }
    void Repeat()
    {
        StartCoroutine(SpawnObj());
    }
    IEnumerator SpawnObj()
    {
        yield return new WaitForSeconds(Timer);
       
        if (IsNextFloor == true)
        {
            yield return new WaitForSeconds(3.0f);
            Timer -= 0.05f;
            IsNextFloor = false;
        }
        float Pos = Random.Range(0.0f, 1.0f);
        if(Pos > 0.5f)
        {
            SpawnPos.position += new Vector3(RightPos,0);
        }
            
        PoolManager.getGameObjectFromPool(Enemy,SpawnPos);

        if (Pos > 0.5f)
        {
            SpawnPos.position -= new Vector3(RightPos, 0);
        }
        Repeat();
    }
}
