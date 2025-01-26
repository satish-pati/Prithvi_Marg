using UnityEngine;
using UnityEngine.UI;

public class Timer1 : MonoBehaviour
{
    public Text timerText;
    public float startTime = 30f; 
    private float currentTime;
    private bool isCounting = true;

    void Start()
    {
        currentTime = startTime;
    }

    void Update()
    {
        if (isCounting)
        {
            currentTime -= Time.deltaTime; 
            if (currentTime <= 0)
            {
                currentTime = 0;
                isCounting = false; 
                TimerFinished(); 
            }
            UpdateTimerDisplay();
        }
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("Time Left: {0:00}:{1:00}", minutes, seconds);
    }

    void TimerFinished()
    {
        Debug.Log("Timer Finished!");
        // Add any action to execute when the timer ends.
    }
}
