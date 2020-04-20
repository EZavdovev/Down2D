using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform FirePoint;
    public GameObject Bullet;
    public float Offset;
    public PlayerController Player;
    public AudioSource ShootSound;

    void Update()
    {
        Vector3 Difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float RotateZ = Mathf.Atan2(Difference.y, Difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, RotateZ + Offset);
            
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(Player.isWatchRight() && Difference.x > 0 || !Player.isWatchRight() && Difference.x < 0)
                Shoot();
        }
    }

    
    void Shoot()
    {
        ShootSound.Play();
        PoolManager.getGameObjectFromPool(Bullet, FirePoint);
        
    }
}
