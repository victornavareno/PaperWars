using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f; // Enemy speed
    [SerializeField] private float rotationSpeed = 2f; // Rotation speed
    [SerializeField] private Transform player; // Player's Transform (target)
    [SerializeField] private AudioClip explosionSound; // Explosion sound

    private Rigidbody2D rb; // Rigidbody2D of the enemy
    private bool gameover = false; // Game state (optional)
    private AudioSource audioSource; // Reference to AudioSource
    private GameManager gameManager; // Reference to the GameManager

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource
        gameManager = FindObjectOfType<GameManager>(); // Find the GameManager in the scene
    }

    void Update()
    {
        if (player != null && !gameover)
        {
            // Calculate direction toward the player
            Vector2 dir = player.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg; // Use Atan2 for the correct angle
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);

            // Smoothly rotate toward the player
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void FixedUpdate()
    {
        if (!gameover)
        {
            // Move forward in the enemy's local axis
            rb.AddRelativeForce(Vector3.right * moveSpeed * Time.fixedDeltaTime);
        }
    }

    public void OnDefeat()
    {
        if (!gameover)
        {
            gameover = true; // Mark as defeated

            // Play the explosion particle effect
            ParticleSystem explosionEffect = GetComponentInChildren<ParticleSystem>();
            if (explosionEffect != null)
            {
                explosionEffect.Play(); // Play the particle system
            }

            // Play the explosion sound
            if (audioSource != null && explosionSound != null)
            {
                audioSource.PlayOneShot(explosionSound); // Play the sound
            }

            // Notify the GameManager that an enemy is defeated
            if (gameManager != null)
            {
                gameManager.EnemyDefeated(); // Notify that an enemy is defeated
            }

            // Destroy the enemy after the explosion effect
            Destroy(gameObject, 3f); // Adjust time based on particle duration
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        // Example: Triggered when hit by a bullet
        if (collision.CompareTag("Bullet"))
        {
            OnDefeat(); // Trigger defeat logic
        }
    }
}
