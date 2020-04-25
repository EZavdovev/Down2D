using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed;
    public Rigidbody2D Rb;

    public void OnEnable()
    {
        Rb.velocity = transform.right * Speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
         PoolManager.putGameObjectToPool(gameObject); 
    }
    
}
