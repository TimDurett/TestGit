﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float speed;
    public float jumpForce;
    private int count;
    public Text countText;
    private int lives;
    public Text livesText;
    public Text winText;
    public Text loseText;
    Animator anim;
    private bool facingRight = true;
    public AudioClip musicClipOne;

    public AudioClip musicClipTwo;

    public AudioSource musicSource;




    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        count = 0;
        lives = 3;
        winText.text = "";
        loseText.text = "";
        SetCountText ();
        musicSource.clip = musicClipOne;
        musicSource.Play();
        musicSource.loop = true;

    }

    // Update is called once per frame
    void Update()
    {
        //Runnning 
       if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetInteger("State", 1);
        } 
       if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetInteger("State", 0);
        }
       if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetInteger("State", 1);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetInteger("State", 0);
        }
        //Jumping

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            anim.SetInteger("State", 2);
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            anim.SetInteger("State", 0);
        }

    }
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector2 movement = new Vector2(moveHorizontal, 0);

        rb2d.AddForce(movement * speed);

        if (Input.GetKey("escape"))
            Application.Quit();

        if (facingRight == false && moveHorizontal > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveHorizontal < 0)
        {
            Flip();
        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                rb2d.AddForce(new Vector2(0, jumpForce),ForceMode2D.Impulse);
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("pickup"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            lives = lives - 1;
            SetCountText();
        }
            if (count == 4)
            {
                transform.position = new Vector3(56.3f, -2.43f, transform.position.z);
            lives = 3;
            SetCountText();
            }

            if (lives == 0)
        {
            Destroy(gameObject);
        }
        }
    void SetCountText()
    {
        countText.text = "Count:" + count.ToString();
      if (count >= 8)
        {
            winText.text = "You Win!";
            musicSource.clip = musicClipTwo;
            musicSource.Play();
        }

        livesText.text = "Lives:" + lives.ToString();
        if (lives == 0)
        {
            loseText.text = "You Lose :.(";
        }
        }
    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;

    }
}
