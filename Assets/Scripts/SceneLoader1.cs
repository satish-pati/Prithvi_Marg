using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader1 : MonoBehaviour
{
    [Header("Target Position for Scene Transition")]
    [SerializeField][Tooltip("The target position that triggers the new scene.")]
    private Vector3 targetPosition = new Vector3(3.81f,0.17f,8.817f);

    [SerializeField][Tooltip("The name of the scene to load additively.")]
    private string additiveSceneName;

    private bool isAdditiveSceneLoaded = false;

    private void Update()
    {
        CheckPlayerPosition();
    }

    private void CheckPlayerPosition()
    {
        // Check if the player's position matches the target position.
        if (Vector3.Distance(transform.position, targetPosition) < 1f && !isAdditiveSceneLoaded)
        {
            LoadScene();
        }
    }

    private void LoadScene()
    {
        if (!string.IsNullOrEmpty(additiveSceneName))
        {
            SceneManager.LoadScene(additiveSceneName);
        }
    }
}
