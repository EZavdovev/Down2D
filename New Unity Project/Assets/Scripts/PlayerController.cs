using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D Rb;
    public float HorizontalSpeed;
    bool IsGrounded, IsLeftGo, IsRightGo, FaceRight;
    public float VerticalImpulse;
    public Animator anim;
    public AudioSource PlayerPlaySound;
    public AudioClip[] PlayerSounds;
    float vertical;
    void Start()
    {
        IsLeftGo = true;
        IsRightGo = true;
        FaceRight = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        vertical = Input.GetAxis("Vertical");
        anim.SetBool("jump", IsGrounded);
        if(anim.GetBool("walk") && anim.GetBool("jump"))
        {

            if (!PlayerPlaySound.isPlaying)
            {
                PlayerPlaySound.clip = PlayerSounds[0];
                PlayerPlaySound.Play();
            }
                
        }
        else
        {
            PlayerPlaySound.Stop();
        }
        if(vertical == 0)
        {
            anim.SetBool("walk", false);
        }

        float speedX = 0.0f;
        if (Input.GetKey(KeyCode.A) && IsLeftGo)
        {
            if (FaceRight == true)
            {
                Flip();
            }
                speedX = HorizontalSpeed;
            anim.SetBool("walk", true);
        }
        if (Input.GetKey(KeyCode.D) && IsRightGo)
        {
            if (FaceRight == false)
            {
                Flip();
            }
                speedX = HorizontalSpeed;
            anim.SetBool("walk", true);
        }

        if(Input.GetKeyDown(KeyCode.Space) && IsGrounded)
        {
            Rb.AddForce(Vector2.up * VerticalImpulse, ForceMode2D.Impulse);
            IsGrounded = false;
            anim.SetBool("jump", true);
        }
        transform.Translate(new Vector3(speedX, 0, 0));
    }

    void Flip()
    {
        FaceRight = !FaceRight;
        transform.Rotate(0f, 180f, 0f);
    }

    public bool isWatchRight()
    {
        return FaceRight;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
            IsGrounded = true;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "LeftWall")
            IsLeftGo = true;
        if (collision.gameObject.tag == "RightWall")
            IsRightGo = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "LeftWall")
            IsLeftGo = false;
        if (collision.gameObject.tag == "RightWall")
            IsRightGo = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "LeftWall")
            IsLeftGo = false;
        if (collision.gameObject.tag == "RightWall")
            IsRightGo = false;
    }

}
