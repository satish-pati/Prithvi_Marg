using UnityEngine;
using UnityEngine.SceneManagement;
public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager instance;
    private Vector3 position=new Vector3(0f, 0f, 16.17f);
    public Vector3 Position
    {
        get { return position; }
        set { position = value; }
    }
    private int i=0;
    public int ix
    {
        get{return i;}
        set{i=value;}
    }
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
        if(scene.name=="Mirror Maze2")
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if(i!=0)
            {
                player.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            if (player != null)
            {
                player.transform.position = position;
            }
        }
    } 
}
