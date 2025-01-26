using UnityEngine;

public class ShootingController : MonoBehaviour
{
    public Camera playerCamera;
    public float shootDistance = 100f;

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // Left mouse button
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, 
                             playerCamera.transform.forward, 
                             out hit, 
                             shootDistance))
        {
            // Check if the hit object has a tag "Enemy"
            if (hit.collider.CompareTag("Enemy"))
            {
                Destroy(hit.collider.gameObject); // Destroy the enemy GameObject
                Debug.Log("Enemy destroyed!");
            }
        }
    }

public LayerMask enemyLayer;

// EnemyHealth Script
public void TakeDamage()
{
    Destroy(gameObject);
}
}
