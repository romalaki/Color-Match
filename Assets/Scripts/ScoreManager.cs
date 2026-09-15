using System;
using Scripts;
using TMPro;
using UnityEngine;
using Zenject;

public class ScoreManager : MonoBehaviour
{
    [Inject]IScoreService score;
    [Inject]LevelDifficulty diff;
    private int plusScore;
    private int minusScore;
    [SerializeField] TextMeshProUGUI[] scoreText;

    private void Start()
    {
        plusScore = diff.diff.give;
        minusScore = diff.diff.take;
        score.NewGame();
        UpdateText();
    }

    public void AddScore()
    {
        score.AddScore(plusScore);
        UpdateText();
    }

    public void SubtractScore()
    {
        score.SubstractScore(minusScore);
        UpdateText();
    }

    private void UpdateText()
    {
        for (int i = 0; i < scoreText.Length; i++)
        {
            scoreText[i].text = "Score: "+score.Score.ToString();
        }
    }

    public void SaveScore()
    {
        score.SaveScore();
    }
    
    void OnApplicationQuit()
    {
        SaveScore();
    }
    
    
}
