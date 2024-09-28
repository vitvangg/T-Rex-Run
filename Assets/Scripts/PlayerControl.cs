using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController character;
    private Vector3 direction;
    public float gravity = 9.81f * 2f;
    public float jumpForce = 10f;
    public float animationSpeedMultiplier = 0.2f;
    private Animator animator;
   

    private void Awake()
    {
        character = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

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
        // Check if the collider is tagged as an obstacle
        if (other.CompareTag("Obstade"))
        {
            // Ngay l?p t?c chuy?n sang ho?t ?nh "Dead"
            animator.SetTrigger("dead");
            // Trigger the GameOver method in the GameManager
            GameManager.Instance.GameOver();
        }
    }
}
