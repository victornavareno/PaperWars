using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;

    void Start()
    {
        // Set the bullet's velocity
        rb.linearVelocity = transform.right * speed;

        // Destroy the bullet after 1 second
        Destroy(gameObject, 1f);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // Check if the bullet hits an enemy
        EnemyMovement enemy = hitInfo.GetComponent<EnemyMovement>();
        if (enemy != null)
        {
            enemy.OnDefeat(); // Call the enemy's destruction logic
        }

        // Destroy the bullet on collision
        Destroy(gameObject);
    }
}
