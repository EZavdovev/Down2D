using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int Hp = 1;
    public Animator anim;
    public CapsuleCollider2D collision;
    public AudioSource EnemyPlaySound;
    public AudioClip[] EnemySounds;
    public void OnEnable()
    {
        Hp = 1;
        collision.enabled = true;
    }
    void Damage()
    {
        Hp--;
        if (Hp <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        GameManager.EnemyDied.Publish("Enemy");
        EnemyPlaySound.clip = EnemySounds[0];
        EnemyPlaySound.Play();
        anim.Play("DieEnemy");
        collision.enabled = false;
        StartCoroutine(DieTime());
       
    }
    IEnumerator DieTime()
    {

        yield return new WaitForSeconds(0.3f);
        PoolManager.putGameObjectToPool(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Rocket")
        {
            Damage();
        }
        if (collision.gameObject.tag == "Bullet")
        {
            Damage();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Boom")
        {
            Damage();
        }
    }
}
