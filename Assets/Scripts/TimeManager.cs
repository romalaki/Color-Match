using System;
using Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class TimeManager : MonoBehaviour
{
    [Inject] LevelDifficulty levelDifficulty;
    private float gameDuration;
    [NonSerialized]public float TimeLeft;
    [NonSerialized]public bool IsRunning;
    [SerializeField] private GameObject Over;
    [SerializeField] private TextMeshProUGUI text;
    
    private void Start()
    {
        gameDuration = levelDifficulty.diff.time;
        TimeLeft = gameDuration;
        StartTimer();
    }

    private void Update()
    {
        if (!IsRunning) return;

        TimeLeft -= Time.deltaTime;

        if (TimeLeft <= 0f)
        {
            TimeLeft = 0f;
            StopGame();
        }
        
        text.text = GetFormattedTime();
    }
    public void StartTimer()
    {
        TimeLeft = gameDuration;
        IsRunning = true;
        Time.timeScale = 1f;
    }
    private void StopGame()
    {
        IsRunning = false;
        Time.timeScale = 0f;
        Over.SetActive(true);
    }
    
    public string GetFormattedTime()
    {
        int seconds = Mathf.CeilToInt(TimeLeft);
        return seconds.ToString();
    }
}