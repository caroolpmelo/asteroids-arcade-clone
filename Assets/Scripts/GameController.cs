using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject asteroid;

    private KeyCode pauseKey = KeyCode.P;

    private int score;
    private int hiscore;
    private int asteroidsRemaining;
    private int lives;
    private int wave;
    private int increaseEachWave = 4;

    public Text scoreText;
    public Text livesText;
    public Text waveText;
    public Text hiscoreText;

    public GameObject btnPauseActive;
    public GameObject btnPauseDefault;

    void Start()
    {
        hiscore = PlayerPrefs.GetInt("hiscore", 0);
        BeginGame();
    }

    void Update()
    {
        // when user press escape
        if (Input.GetKey("escape"))
            Application.Quit();

        // pause game
        if (Input.GetKeyDown(pauseKey))
            PauseGame();
    }

    void BeginGame()
    {
        score = 0;
        lives = 3;
        wave = 1;

        // prepare hud
        scoreText.text = "SCORE:" + score;
        hiscoreText.text = "HISCORE:" + hiscore;
        livesText.text = "LIVES:" + lives;
        waveText.text = "WAVE:" + wave;

        SpawnAsteroids();
    }

    void SpawnAsteroids()
    {
        DestroyExistingAsteroids();

        // decide how many asteroids spawn
        // if any is leftover from previous play, subtract
        asteroidsRemaining = (wave * increaseEachWave);

        for (int i = 0; i < asteroidsRemaining; i++)
        {
            // spawn asteroid
            Instantiate(
                asteroid,
                new Vector3(
                    Random.Range(-9.0f, 9.0f),
                    Random.Range(-6.0f, 6.0f),
                    0
                ),
                Quaternion.Euler(0, 0, Random.Range(-0.0f, 359.0f))
            );

            waveText.text = "WAVE:" + wave;
        }
    }

    public void IncrementScore()
    {
        score++;

        scoreText.text = "SCORE:" + score;

        if (score > hiscore)
        {
            hiscore = score;
            hiscoreText.text = "HISCORE:" + hiscore;

            // save new hiscore
            PlayerPrefs.SetInt("hiscore", hiscore);
        }

        if (asteroidsRemaining < 1)
        {
            wave++; // new wave
            SpawnAsteroids();
        }
    }

    public void DecrementLives()
    {
        lives--;
        livesText.text = "LIVES:" + lives;

        if (lives < 1) // run out of lives
            BeginGame(); // restart
    }

    public void DecrementAsteroids()
    {
        asteroidsRemaining--;
    }

    public void SplitAsteroid()
    {
        // extra asteroids
        // - big one + 3 little = 2
        asteroidsRemaining += 2;
    }

    void DestroyExistingAsteroids()
    {
        GameObject[] largeAsteroids =
            GameObject.FindGameObjectsWithTag("Large Asteroid");

        foreach (GameObject current in largeAsteroids)
            GameObject.Destroy(current);

        GameObject[] smallAsteroids =
            GameObject.FindGameObjectsWithTag("Small Asteroid");

        foreach (GameObject current in smallAsteroids)
            GameObject.Destroy(current);
    }

    void PauseGame()
    {
        if (btnPauseActive.activeSelf)
        { // is paused
            btnPauseActive.SetActive(false);
            btnPauseDefault.SetActive(true);
            Time.timeScale = 1; // normal time
        }
        else if (btnPauseDefault.activeSelf)
        { // is not paused
            btnPauseDefault.SetActive(false);
            btnPauseActive.SetActive(true);
            Time.timeScale = 0; // freezes time
        }
    }
}
