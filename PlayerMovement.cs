using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Include UI namespace for working with Text
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1.5f; // Base movement speed
    [SerializeField] private float rotationSpeed = 100f; // Rotation speed
    [SerializeField] private float slowSpeedFactor = 0.5f; // Slowdown factor while charging nitro
    [SerializeField] private float boostMultiplier = 3f; // Speed boost multiplier when space is released
    [SerializeField] private float boostDuration = 2f; // Duration of the boost (in seconds)

    [SerializeField] private GameObject gameOverText; // Reference to the GameOverText GameObject
    [SerializeField] private float slowDownDuration = 3f; // Duration for how long the player stays slowed down after hitting a square

    private bool gameover = false; // Game state
    private Rigidbody2D rb; // Rigidbody2D component
    private Camera cam; // Camera that follows the player

    private float currentSpeed; // Current speed of the player
    private bool isCharging = false; // Is the player holding the spacebar?
    private bool isSlowedDown = false; // Is the player currently slowed down by a square?

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Assign Rigidbody2D component
        cam = Camera.main; // Get the main camera

        // Ensure the GameOverText is hidden at the start
        if (gameOverText != null)
        {
            gameOverText.SetActive(false);
        }

        // Set the current speed to the base move speed initially
        currentSpeed = moveSpeed;
    }

    void Update()
    {
        // Check if the ESC key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Load the "Main Menu" scene
            SceneManager.LoadScene("Main Menu");
        }

        if (!gameover)
        {
            HandleRotation(); // Handle player rotation
            HandleNitro(); // Handle nitro mechanics
        }
    }

    private void HandleRotation()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.forward * -rotationSpeed * Time.deltaTime);
        }
    }

    private void HandleNitro()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            // Charging nitro: slow down the ship while holding space
            isCharging = true;
            currentSpeed = moveSpeed * slowSpeedFactor; // Reduce speed by the slowSpeedFactor
        }
        else if (isCharging)
        {
            // When space is released: apply boost and start the coroutine to reset speed
            isCharging = false;
            StartCoroutine(BoostSpeed(boostMultiplier, boostDuration));
        }
        else
        {
            // If space is not held down, use the normal speed
            currentSpeed = moveSpeed;
        }
    }

    private IEnumerator BoostSpeed(float multiplier, float duration)
    {
        // Boost speed by multiplier
        currentSpeed = moveSpeed * multiplier;

        // Wait for the specified boost duration
        yield return new WaitForSeconds(duration);

        // After boost ends, reset speed to normal
        currentSpeed = moveSpeed;
    }

    void FixedUpdate()
    {
        if (!gameover)
        {
            // Apply forward force with the adjusted movement speed
            rb.AddRelativeForce(Vector2.right * currentSpeed * Time.fixedDeltaTime);
        }
    }

    void LateUpdate()
    {
        if (!gameover && cam != null)
        {
            // Follow player with the camera
            cam.transform.position = new Vector3(transform.position.x, transform.position.y, cam.transform.position.z);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !gameover)
        {
            // If the player collides with an enemy, the game ends
            gameover = true;
            GetComponent<SpriteRenderer>().enabled = false; // Hide the player sprite
            GetComponent<PolygonCollider2D>().enabled = false; // Disable the collider

            GetComponentInChildren<ParticleSystem>().Play(); // Play explosion effect

            // Show the "ELIMINADO!" message
            if (gameOverText != null)
            {
                gameOverText.SetActive(true); // Enable the GameOverText
            }

            // Delay for 2 seconds and then return to the main menu
            StartCoroutine(ShowGameOver());
        }
        
    }

    private IEnumerator SlowDownPlayer()
    {
        isSlowedDown = true; // Set the flag to indicate the player is slowed down

        // Reduce the player's speed
        currentSpeed = moveSpeed * slowSpeedFactor;

        // Wait for the specified slow-down duration
        yield return new WaitForSeconds(slowDownDuration);

        // Reset the player's speed to normal
        currentSpeed = moveSpeed;

        isSlowedDown = false; // Allow further slow-downs
    }

    private IEnumerator ShowGameOver()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Main Menu");
    }
}
