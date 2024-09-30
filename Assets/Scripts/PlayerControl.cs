using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController character;
    private Vector3 direction;
    public float gravity = 9.81f * 2f;
    public float jumpForce = 2f;
    public float animationSpeedMultiplier = 0.2f;
    private Animator animator;
    private AudioManager audioManager;
    [SerializeField] ParticleSystem eatEffect;
    private bool isEatItem = false;

    private void Awake()
    {
        audioManager = GameObject.FindWithTag("Audio").GetComponent<AudioManager>();
        character = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        eatEffect = GetComponentInChildren<ParticleSystem>();

    }
    private void OnEnable()
    {
        direction = Vector3.zero;
    }
    private void Update()
    {
        float gameSpeed = GameManager.Instance.gameSpeed;
        float runAnimationSpeed = gameSpeed * animationSpeedMultiplier;

        direction += Vector3.down * gravity * Time.deltaTime;
        if (!GameManager.Instance.isGameOver)
        {
            if (character.isGrounded)
            {
                direction = Vector3.down;
                animator.SetBool("isGrounded", true);
                animator.speed = runAnimationSpeed;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    audioManager.PlayJump(audioManager.jumpClip);
                    animator.speed = 1.0f;
                    direction = Vector3.up * jumpForce;
                    animator.SetBool("isGrounded", false);
                }
            }
        }
        // Check if the game is over
        if (GameManager.Instance.isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Call the NewGame method to initialize the game
                GameManager.Instance.NewGame();
            }
        }
        character.Move(direction * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {

        // Khi chạm vào item, nhân vật bay lên và game x2 tốc độ trong vài giây
        if (other.CompareTag("item"))
        {
            eatEffect.Play();
            animator.SetBool("isGrounded", true);
            Destroy(other.gameObject);
            audioManager.PlayItem(audioManager.itemClip);

            // Bay lên
            direction = Vector3.up * jumpForce * 2f; // Tăng lực nhảy
            // Tăng tốc độ game gấp đôi
            isEatItem = true;
        }
        // Check if the collider is tagged as an obstacle
        if (other.CompareTag("Obstade"))
        {
            if (isEatItem == true)
            {
                Destroy(other.gameObject);
                eatEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                isEatItem = false;
            }
            else
            {
                // "Dead"
                animator.SetTrigger("dead");
                audioManager.PlaySFX(audioManager.deadClip);
                // Trigger the GameOver method in the GameManager
                GameManager.Instance.GameOver();
            }
        }
    }
}
