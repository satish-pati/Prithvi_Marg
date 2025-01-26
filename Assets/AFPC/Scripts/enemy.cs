using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject targetCube;
    private bool canMove = true;

    void Update()
    {
        if (!canMove || targetCube == null) return;

        // Calculate direction to target
        Vector3 direction = (targetCube.transform.position - transform.position).normalized;

        // Move towards target
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Stop when reaching target cube
        if (other.gameObject == targetCube)
        {
            canMove = false;
        }
    }
}