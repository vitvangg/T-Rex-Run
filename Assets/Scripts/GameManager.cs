using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float initalGameSpeed = 10f; // toc do ban dau 
    public float gameSpeedIncrease = 0.5f;
    public float gameSpeed;

    private Spawner spawner;
    private CoinController item;
    private PlayerController player;
    private AudioManager audioManager;

    [Space(10)]
    // Animator component reference
    public Animator animator;

    [Space(10)]
    // UI elements for game start and game over
    public TextMeshProUGUI gameStartText;
    public TextMeshProUGUI gameOverText;
    public Button retryButton;

    [Space(10)]
    // UI elements for displaying score and high score
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;
    public TextMeshProUGUI Hi;


    public bool isGameStarted = false;
    public bool isGameOver = false;
    private float score;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
        audioManager = GameObject.FindWithTag("Audio").GetComponent<AudioManager>();
    }
    private void OnDestroy()
    {
        if (Instance == null)
        {
            Instance = null;
        }
    }
    private void Start()
    {
        // Hide the mouse cursor at the beginning of the game
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Reset HighScore to 0 when the game starts
        PlayerPrefs.DeleteKey("HighScore");
        // Hide high score and "Hi" text
        highscoreText.gameObject.SetActive(false);
        Hi.gameObject.SetActive(false);
        player = FindObjectOfType<PlayerController>();
        spawner = FindObjectOfType<Spawner>();
        item = FindObjectOfType<CoinController>();

        gameStartText.gameObject.SetActive(true);

        animator.SetBool("isGameStarted", false);
        // Set up the game speed increase to 0
        gameSpeedIncrease = 0;

        // Deactivate the obstacle spawner                                   
        spawner.gameObject.SetActive(false);
        item.gameObject.SetActive(false);
    }

    public void GameStart()
    {
        // Hide game over UI elements
        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);

        // Check if the initial jump has been performed and the game hasn't started yet
        if (Input.GetKeyDown(KeyCode.Space) && !isGameStarted)
        {
            isGameStarted = true;
            // Call the NewGame method to initialize the game
            NewGame();
            // Hide game start UI elements and reset animation state
            gameStartText.gameObject.SetActive(false);
            animator.SetBool("isGameStarted", true);
        }

    }
    public void NewGame()
    {
        audioManager.PlayMusic(audioManager.musicClip);
        gameSpeedIncrease = 0.1f;

        // Deactivate the player to reset animation
        player.gameObject.SetActive(false);

        // Reset the score to 0 when starting a new game
        score = 0;
        scoreText.text = Mathf.FloorToInt(score).ToString("D5");

        Pipe[] pipes = FindObjectsOfType<Pipe>();
        foreach (var pipe in pipes)
        {
            Destroy(pipe.gameObject);
        }

        gameSpeed = initalGameSpeed;
        enabled = true;
        player.gameObject.SetActive(true);
        spawner.gameObject.SetActive(true);
        item.gameObject.SetActive(true);


        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
        // Reset the game over and dead animation state
        isGameOver = false;
        animator.SetBool("isGameStarted", true);

    }
    public void GameOver()
    {
        gameSpeed = 0f;
        // D?ng nh?c khi game over
        audioManager.musicAudioSource.Stop();


        animator.SetBool("isGameOver", true);
        enabled = false;

        spawner.gameObject.SetActive(false);
        item.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);
        Hi.gameObject.SetActive(true);
        highscoreText.gameObject.SetActive(true);
        isGameOver = true;
        UpdateHighScore();
    }
    private void Update()
    {
        if (!isGameOver)
        {
            GameStart();
            gameSpeed += gameSpeedIncrease * Time.deltaTime;
            score += gameSpeed * Time.deltaTime;
            scoreText.text = Mathf.FloorToInt(score).ToString("D5");
        }

    }
    private void UpdateHighScore()
    {
        float highscore = PlayerPrefs.GetFloat("HighScore", 0);
        if (score > highscore)
        {
            highscore = score;
            PlayerPrefs.SetFloat("HighScore", highscore);
        }
        highscoreText.text = Mathf.FloorToInt(highscore).ToString("D5");
    }
}
