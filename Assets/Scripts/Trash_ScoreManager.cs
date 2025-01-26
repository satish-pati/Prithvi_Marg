using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Trash_ScoreManager : MonoBehaviour
{
    public static Trash_ScoreManager instance;
    public int score = 0;
    public TMP_Text scoreText;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = "Score: " + score;
    }
}