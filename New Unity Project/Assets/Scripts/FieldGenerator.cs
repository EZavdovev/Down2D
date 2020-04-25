using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldGenerator : MonoBehaviour
{
    public GameObject Fields;
    public GameObject FirstField;
    Vector3 Pos;
    float FieldYPos = 0.0f;
    float FieldXPos = 0.0f;
    int FieldCount = 4;
    float FieldLength = 5.29f;
    List<GameObject> CurrentFields = new List<GameObject>();

    void Start()
    {
        GameManager.PlayerFall.Subscribe(ChangeLevel);
        CurrentFields.Add(FirstField);
        Pos = FirstField.transform.position;
        FieldYPos = Pos.y;
        FieldXPos = Pos.x;
        for (int i = 0; i < FieldCount; i++)
            SpawnField();
    }

    public void ChangeLevel(string type_change)
    {
        if(type_change == "Fall")
        {
            SpawnField();
            DestroyField();
        }
    }

    void SpawnField()
    { 
        GameObject Field = Instantiate(Fields,Pos,Quaternion.identity);
        FieldYPos -= FieldLength;
        Field.transform.position = new Vector3(FieldXPos, FieldYPos);
        CurrentFields.Add(Field);
    }

    void DestroyField()
    {
        StartCoroutine(DestroyControl());
    }

    IEnumerator DestroyControl()
    {
        yield return new WaitForSeconds(2.0f);
        Destroy(CurrentFields[0]);
        CurrentFields.RemoveAt(0);
    }
}
