using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBreak : MonoBehaviour
{
    public CircleCollider2D BoomControl;
    public Animator BoomAnim;
    public SpriteRenderer BoomSprite;
    public CapsuleCollider2D RocketDestroyer;
    public SpriteRenderer RocketSprite;
    public AudioSource RocketPlaySound;
    public AudioClip[] RocketSounds;// 0-полет ; 1 - взрыв.

    private void OnEnable()
    {
        RocketDestroyer.enabled = true;
        RocketSprite.enabled = true;
        BoomControl.enabled = false;
        BoomAnim.enabled = false;
        BoomSprite.enabled = false;
        RocketPlaySound.clip = RocketSounds[0];
        RocketPlaySound.Play();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
            RocketCollideBehaviour(collision); 
        }

        if (collision.gameObject == GameManager.player.gameObject)
        {
            GameManager.player.Damage();
        }
    }
    
    private void RocketCollideBehaviour(Collision2D collision)
    {
        Destroy(collision.gameObject);
        RocketSprite.enabled = false;
        RocketDestroyer.enabled = false;
        BoomControl.enabled = true;
        BoomSprite.enabled = true;
        BoomAnim.enabled = true;
        RocketPlaySound.clip = RocketSounds[1];
        RocketPlaySound.Play();
        StartCoroutine(BoomTime());
    }

    IEnumerator BoomTime()
    {
        yield return new WaitForSeconds(0.8f);
        PoolManager.putGameObjectToPool(gameObject);
    }
}
