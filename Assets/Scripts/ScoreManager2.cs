using UnityEngine;
using UnityEngine.UI;
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
    }
    public void IncScore()
    {
        score+=10;
    }
}
