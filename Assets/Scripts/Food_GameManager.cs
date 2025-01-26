using UnityEngine;
using UnityEngine.SceneManagement;
public class Food_GameManager : MonoBehaviour
{
    public static Food_GameManager instance;
    [SerializeField]
    private GameObject[] Animals;
    [SerializeField]
    private Vector3[] positions;
    void Awake()
    {
        if(instance==null)
        {
            instance=this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded+=OnLevelFinishedLoading;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded-=OnLevelFinishedLoading;
    }
    void OnLevelFinishedLoading(Scene scene,LoadSceneMode mode)        
    {
        if(scene.name=="Food_Chain")
        {
            Vector3[] shuffledPositions = ShufflePositions(positions);
            for(int i=0;i<Animals.Length;i++)
            {
                Instantiate(Animals[i], shuffledPositions[i], Quaternion.identity);
            }
        }
    }
    private Vector3[] ShufflePositions(Vector3[] originalPositions)
    {
        Vector3[] shuffled = (Vector3[])originalPositions.Clone();
        for (int i = 0; i < shuffled.Length; i++)
        {
            int randomIndex = Random.Range(i, shuffled.Length);
            // Swap positions
            Vector3 temp = shuffled[i];
            shuffled[i] = shuffled[randomIndex];
            shuffled[randomIndex] = temp;
        }
        return shuffled;
    } 
}
