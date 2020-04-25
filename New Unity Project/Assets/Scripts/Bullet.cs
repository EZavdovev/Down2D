﻿using System.Collections;
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
         PoolManager.putGameObjectToPool(gameObject); //Любая коллизия уничтожит объект, нет смысла искать
         
        /*if (collision.gameObject.tag == "Enemy")
        {
            PoolManager.putGameObjectToPool(gameObject);

        }
        if (collision.gameObject.tag == "Platform")
        {
            PoolManager.putGameObjectToPool(gameObject);

        }
        if (collision.gameObject.tag == "LeftWall")
        {
            PoolManager.putGameObjectToPool(gameObject);
        }
        if (collision.gameObject.tag == "RightWall")
        {
            PoolManager.putGameObjectToPool(gameObject);
        }
        if (collision.gameObject.tag == "Rocket")
        {
            PoolManager.putGameObjectToPool(gameObject);
        }*/
    }
    
}
