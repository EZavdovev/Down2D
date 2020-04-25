using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTravel : MonoBehaviour
{
    float ChangePos = 5.29f;
    public float SpeedCamera;

    void Start()
    {
        GameManager.PlayerFall.Subscribe(Travel);
    }

    public void Travel(string type_travel)
    {
        if (type_travel == "Fall")
        {
            StartCoroutine(SlowMove());
        }
    }

    IEnumerator SlowMove()
    {
        float Control = 0.0f;
        while (Control < ChangePos)
        {
            yield return new WaitForSeconds(0.0001f);
            if (Control + SpeedCamera < ChangePos)
                transform.Translate(new Vector3(0, -SpeedCamera));
            else
                transform.Translate(new Vector3(0, -(SpeedCamera - ((Control + SpeedCamera) - ChangePos))));
            Control += SpeedCamera;
        }
    }
}
