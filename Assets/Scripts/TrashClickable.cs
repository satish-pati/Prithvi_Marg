using UnityEngine;

public class TrashClickHandler : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))  
        {
           Trash_ScoreManager.instance.AddScore(10);

            Destroy(gameObject);
        }
    }
}
