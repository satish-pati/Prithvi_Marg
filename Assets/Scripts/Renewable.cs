using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Renewable : MonoBehaviour
{
    private bool isWaiting = false;

    void Update()
    {
        if (!isWaiting && transform.position.z > 25)
        {
            StartCoroutine(StopAfterDelay());
        }
    }

    private IEnumerator StopAfterDelay()
    {
        isWaiting = true; // Prevent multiple coroutines from running simultaneously
        yield return new WaitForSeconds(1f); // Wait for 2 seconds

        Vector3 position1 = new Vector3(-4.1f, 0.17f, -0.21f);
        GameSceneManager.instance.Position=position1;
        SceneManager.LoadScene("Mirror Maze2");
    }
}
