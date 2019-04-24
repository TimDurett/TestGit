using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject [] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public int timeLeft = 30;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public AudioSource musicSource;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;


    public Text ScoreText;
    public Text restartText;
    public Text gameoverText;
    public Text winText;
    public Text countdownText;

    private bool gameover;
    private bool restart;
    
    private int score;

    public GameObject obj;
     void Awake()
    {
        obj = GameObject.FindGameObjectWithTag("Background");
    }
    void Start()
    {
        
        gameover = false;
        restart = false;
        restartText.text = "";
        gameoverText.text = "";
        winText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
        StartCoroutine(LoseTime());
    }

     void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SceneManager.LoadScene("Ex Space Shooter");
            }
        }
        if (Input.GetKey("escape"))
            Application.Quit();
        countdownText.text = "Time Left:" + timeLeft;
        if (timeLeft <= 0)
        {
            StopCoroutine(LoseTime());
            countdownText.text = "Times Up!!";
            gameoverText.text = "Game Over!! and made by Tim Durett";
            gameover = true; 
        }
        
    }
    IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;

            if (gameover)
            {
                StopCoroutine(LoseTime());
                restartText.text = "Press'Q'for Restart";
                restart = true;
                break;
            }
        }

    }
    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
            if (gameover)
            {
                restartText.text = "Press'Q'for Restart";
                restart = true;
                break;
            }
           
        }
    }
    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        ScoreText.text = "Points: " + score;
        if (score >= 100)
        {
            winText.text = "You win! and made by Tim Durett";
            obj.GetComponent<BGMover>().scrollSpeed += -2;
            StopCoroutine(LoseTime());
            StopCoroutine(SpawnWaves());
            musicSource.clip = musicClipOne;
            musicSource.Play();
            gameover = true;
            restart = true;
        }
    }
    public void GameOver()
    {
        gameoverText.text = "Game Over!! and made by Tim Durett";
        musicSource.clip = musicClipTwo;
        musicSource.Play();
        gameover = true;
    }
}