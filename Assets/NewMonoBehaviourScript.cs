using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour

    // Start is called once before the first execution of Update after the MonoBehaviour is created
{
    public float velocity = 5f;
    public Vector3 moveDirection = Vector3.forward;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        // Verify the direction
        if (moveDirection == Vector3.forward)
            moveDirection = transform.forward; // Uses cube's actual forward direction
        
        rb.linearVelocity = moveDirection * velocity;
    }

    void Update()
    {
        // Ensure consistent velocity
        rb.linearVelocity = moveDirection * velocity;
    }
}