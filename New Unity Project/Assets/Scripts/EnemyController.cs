using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float Speed;
    public float RayDistance;
    Transform Target;
    public Rigidbody2D Rb;
    float VerticalImpulse;
    bool FaceRight;
    bool IsGrounded;
    public Animator anim;
    public AudioSource EnemyMoveSound;
    public AudioSource EnemyHitSound;
    public AudioClip[] EnemySounds;// 0-ходьба; 1-атака.

    void OnEnable()
    {
        VerticalImpulse = 7.0f;
        Physics2D.queriesStartInColliders = false;
        FaceRight = true;
        IsGrounded = false;
        anim.SetBool("jump", IsGrounded);
        StartCoroutine(SpawnTime());
    }

    IEnumerator SpawnTime()
    {
        
        yield return new WaitForSeconds(0.5f);
        if (GameObject.FindGameObjectWithTag("Player") != null)
            Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
   
    void Update()
    {
        if (Target != null)
        {
            if (anim.GetBool("jump") && !anim.GetBool("hit"))
            {
                if (!EnemyMoveSound.isPlaying)
                {
                    EnemyMoveSound.clip = EnemySounds[0];
                    EnemyMoveSound.Play();
                }
            }
            else
            {
                EnemyMoveSound.Stop();
            }
        
            if (anim.GetBool("hit"))
            {
                if (!EnemyHitSound.isPlaying)
                {
                    EnemyHitSound.clip = EnemySounds[1];
                    EnemyHitSound.Play();
                }
            }
            else
            {
                EnemyHitSound.Stop();
            }
        
            anim.SetBool("jump", IsGrounded);
            if (Target.transform.position.x > transform.position.x)
            {
                if (!FaceRight)
                    Flip();
            }
            else
            {
                if (FaceRight)
                    Flip();
            }

            transform.position = Vector2.MoveTowards(transform.position, Target.position, Speed * Time.deltaTime);
            if (FaceRight == true)
            {
                RaycastHit2D Hit = Physics2D.Raycast(transform.position, transform.localScale.x * Vector3.right, RayDistance);
                Debug.Log(Hit.collider);
                if (Hit.collider != null && Hit.collider.gameObject.tag == "Platform" && IsGrounded)
                {
                    anim.SetBool("jump", true);
                    IsGrounded = false;
                    Rb.AddForce(Vector2.up * VerticalImpulse, ForceMode2D.Impulse);
                }
                if (Hit.collider != null && Hit.collider.gameObject.tag == "Player")
                {
                    anim.SetBool("hit", true);
                }
                else
                {
                    anim.SetBool("hit", false);
                }
            }
            else
            {
                RaycastHit2D Hit = Physics2D.Raycast(transform.position, transform.localScale.x * Vector3.left, RayDistance);
                if (Hit.collider != null && Hit.collider.gameObject.tag == "Platform" && IsGrounded)
                {
                    anim.SetBool("jump", true);
                    IsGrounded = false;
                    Rb.AddForce(Vector2.up * VerticalImpulse, ForceMode2D.Impulse);
                }
                if (Hit.collider != null && Hit.collider.gameObject.tag == "Player")
                {
                    anim.SetBool("hit", true);
                }
                else
                {
                    anim.SetBool("hit", false);
                }
            }   
        }
    }

    void Flip()
    {
        FaceRight = !FaceRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.localScale.x * Vector3.right * RayDistance);
        Gizmos.DrawLine(transform.position, transform.position + transform.localScale.x * Vector3.left * RayDistance);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            IsGrounded = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
            Rb.AddForce(Vector2.up * VerticalImpulse, ForceMode2D.Impulse);
        }
    }
}
