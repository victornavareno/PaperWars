using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    private AudioSource audioSource; // Reference to the AudioSource

    void Start()
    {
        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check for key press (not hold)
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Instantiate the bullet at the firePoint
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Play the shooting sound
        audioSource.Play();
    }
}
