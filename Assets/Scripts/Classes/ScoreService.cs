using UnityEngine;

namespace Scripts
{
    public class ScoreService : IScoreService
    {
        public int Score { get; private set; }
        public int BestScore (){ return PlayerPrefs.GetInt("BestScore", 0); }
        public void AddScore(int value) { Score += value; }
        public void SubstractScore(int value) { Score -= value; }
        public void NewGame() { Score = 0; }
        
        public void SaveScore()
        {
            if (Score > BestScore()) 
                PlayerPrefs.SetInt("BestScore", Score);
        }


    }
}