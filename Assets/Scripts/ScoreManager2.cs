using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ScoreMangager1 : MonoBehaviour
{
    [SerializeField]
    private Text Score;
    public int score=0;
    
    void OnEnable()
    {
        DrawWithMouse3D1.ScoreUpdate1+=IncScore;
    }
    void OnDisable()
    {
        DrawWithMouse3D1.ScoreUpdate1-=IncScore;
    }
    void Update()
    {
        if(Score!=null)
        Score.text="Score: "+score.ToString()+"/120";
        if(score==120)
        {
            Vector3 position1 = new Vector3(0.2f, 0.17f, 8.817f);
            GameSceneManager.instance.Position=position1;
            SceneManager.LoadScene("Mirror Maze2");
        }
    }
    public void IncScore()
    {
        score+=10;
    }
}
