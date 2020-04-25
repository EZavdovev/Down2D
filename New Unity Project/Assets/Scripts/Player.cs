using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private int Hp = 3;
    private int Maxhp = 3;
    private bool GodMod = false;
    public Image[] Lives;
    public Sprite FullLive;
    public Sprite EmptyLive;
    public SpriteRenderer Color_Player;
    public AudioSource PlayerPlaySound;
    public AudioClip[] PlayerSounds;

    private void FixedUpdate()
    {
       
        for (int i = 0; i < Lives.Length; i++)
        {
            if(i < Hp)
            {
                Lives[i].sprite = FullLive;
            }
            else
            {
                Lives[i].sprite = EmptyLive;
            }

            if(i < Maxhp)
            {
                Lives[i].enabled = true;
            }
            else
            {
                Lives[i].enabled = false;
            }
        }
    }

    IEnumerator GodTime()
    {
        float timer = 0.0f;
        PlayerPlaySound.clip = PlayerSounds[0];
        PlayerPlaySound.Play();
        while (timer < 2.0f)
        {
            yield return new WaitForSeconds(0.1f);
            Color_Player.color = Color.black;
            yield return new WaitForSeconds(0.1f);
            timer += 0.2f;
            Color_Player.color = Color.white;
        }
        GodMod = false;
    }
    void Damage()
    {
        if (!GodMod)
        {
            GodMod = true;
            
            StartCoroutine(GodTime());
            Hp--;
            if (Hp <= 0)
            {
                Die();
            }
        }
    }
    void Die()
    {
        Destroy(gameObject);
        GameManager.EventClean.Clear();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    
    //Добавь объекты для коллизий в инспекторе и сделай проверку аналогично ChunkTrigger.cs 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Damage();
        }
        if (collision.gameObject.tag == "Rocket")
        {
            Damage();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Boom")
        {
            Damage();
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Damage();
        }
        if (collision.gameObject.tag == "Rocket")
        {
            Damage();
        }
    }
}
