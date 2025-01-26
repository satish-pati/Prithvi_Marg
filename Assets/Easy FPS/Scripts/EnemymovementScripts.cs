using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyTargetScript : MonoBehaviour 
{
    [Tooltip("Movement speed of the enemy")]
    public float moveSpeed = 5f;

    private Rigidbody rb;
    private Camera mainCamera;
    private float shootTimer = 0f;
    private Vector3 movementDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;

        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.useGravity = false;
        }

        movementDirection = Vector3.right;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movementDirection * moveSpeed;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            movementDirection *= -1;
        }
    }

    void Update()
    {
        bool isGoodCube = gameObject.CompareTag("GoodCube");
        bool isBadCube = gameObject.CompareTag("BadCube");
        bool isAimedAt = IsAtScreenCenter();
        
        if (isAimedAt && Input.GetMouseButton(0))
        {
            shootTimer += Time.deltaTime;

            if (shootTimer >= 1.5f)
            {
                if (isBadCube)
                {
                    Destroy(gameObject);
                }
                else if (isGoodCube)
                {
                    // Completely freeze game, requiring restart
                    Application.Quit();
                    #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
                    #endif
                }
            }
        }
        else
        {
            shootTimer = 0f;
        }
    }

    bool IsAtScreenCenter()
    {
        if (mainCamera == null) return false;

        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Vector3 screenPoint = mainCamera.WorldToScreenPoint(transform.position);
        
        return Mathf.Abs(screenPoint.x - screenCenter.x) < 50f &&
               Mathf.Abs(screenPoint.y - screenCenter.y) < 50f &&
               screenPoint.z > 0;
    }
}