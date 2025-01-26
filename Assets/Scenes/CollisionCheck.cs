using System.Collections.Generic;
using UnityEngine;
using TMPro; // Import TextMeshPro

public class CollisionCheck : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public ParticleSystem particleSystem1;
    public ParticleSystem particleSystem2;
    public ParticleSystem particleSystem3;

    [SerializeField] private GameObject playerObject;
    [SerializeField] public TextMeshProUGUI timerText; // UI Timer Text
    [SerializeField] private float playerHeight = 2.0f;
    [SerializeField] private float playerRadius = 0.5f;
    [SerializeField] private Vector3 moveDir = Vector3.forward;
    [SerializeField] private float moveDistance = 1.0f;

    private bool canMove;
    private HashSet<string> collidedTags = new HashSet<string>(); // Track unique collisions
    private HashSet<string> validTags = new HashSet<string> { "door", "door1", "door2", "door3" };

    private float gameTimer = 0f;
    private float gameDuration = 120f; // 2 minutes

    void Update()
    {
        if (playerObject == null || timerText == null)
        {
            Debug.LogWarning("Player object or Timer Text is not assigned!");
            return;
        }

        // Update game timer
        gameTimer += Time.deltaTime;
        float timeRemaining = Mathf.Max(0, gameDuration - gameTimer);
        UpdateTimerUI(timeRemaining);

        if (timeRemaining <= 0)
        {
            GameOver();
            return;
        }

        RaycastHit hit;
        canMove = !Physics.CapsuleCast(
            playerObject.transform.position,
            playerObject.transform.position + Vector3.up * playerHeight,
            playerRadius,
            moveDir,
            out hit,
            moveDistance
        );

        if (!canMove)
        {
            OnCollisionDo(hit);
        }
    }

    private void UpdateTimerUI(float timeRemaining)
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = $"Time Left: {minutes:00}:{seconds:00}";
    }

    private void OnCollisionDo(RaycastHit hit)
    {
        string hitTag = hit.collider.tag;
        if (!validTags.Contains(hitTag))
        {
            return;
        }

        if (hit.collider.CompareTag("door"))
        {
            StartCoroutine(PlayParticleSystem(particleSystem));
        }
        if (hit.collider.CompareTag("door1"))
        {
            StartCoroutine(PlayParticleSystem(particleSystem1));
        }
        if (hit.collider.CompareTag("door2"))
        {
            StartCoroutine(PlayParticleSystem(particleSystem2));
        }
        if (hit.collider.CompareTag("door3"))
        {
            StartCoroutine(PlayParticleSystem(particleSystem3));
        }

        if (!collidedTags.Contains(hitTag))
        {
            collidedTags.Add(hitTag);
            Debug.Log("Collided with: " + hitTag);
        }

        // Check if we have hit 2 unique objects
        if (collidedTags.Count >= 2)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over!");
        Time.timeScale = 0f; // Freeze the game
        timerText.text = "Game Over!"; // Show game over text
    }

    private System.Collections.IEnumerator PlayParticleSystem(ParticleSystem ps)
    {
        if (ps != null)
        {
            ps.Play();
            yield return new WaitForSeconds(1f);
            ps.Stop();
        }
        else
        {
            Debug.LogWarning("Particle System is not assigned!");
        }
    }
}
