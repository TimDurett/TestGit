using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Text countText;
    public Text winText;
    public Text scoreText;
    public Text loseText;
    public Text livesText;

    private Rigidbody rb;
    private int count;
    private int score;
    private int lives;

     void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        lives = 3;
        SetCountText();
        winText.text = "";
        loseText.text = "";
        
        
    }
     void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);


        rb.AddForce(movement * speed);

        if (Input.GetKey("escape"))
            Application.Quit();

    }

    // Destroy everything that enters the trigger
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            score = score + 1;
            SetCountText();
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            lives = lives - 1;
            score = score - 1;// this removes 1 from the score
            SetCountText();
        }
        if (count == 12)
        {
            transform.position = new Vector3(51.63f, -15.55f, -13.7f);
        }
        if (lives <= 0)
        {
            Destroy(gameObject);
        }
    }
    void SetCountText ()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 20)
        {
            winText.text = "You Win!";
        }

        livesText.text = "Lives: " + lives.ToString();
        if (lives <= 0)
        {
            loseText.text = "You Lose!:(";
        }
        scoreText.text = "Score: " + score.ToString();
        
    }
}