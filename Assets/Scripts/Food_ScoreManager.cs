using UnityEngine;
using UnityEngine.UI;
public class Food_ScoreMangager : MonoBehaviour
{
    [SerializeField]
    private Text Score;
    public int score=0;
    
    void OnEnable()
    {
        DrawWithMouse3D.ScoreUpdate+=IncScore;
    }
    void OnDisable()
    {
        DrawWithMouse3D.ScoreUpdate-=IncScore;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        if(Score!=null)
        Score.text="Score: "+score.ToString()+"/50";
    }
    public void IncScore()
    {
        score+=10;
    }
}
