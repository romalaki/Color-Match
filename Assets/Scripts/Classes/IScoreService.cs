namespace Scripts
{
    public interface IScoreService
    {
        int Score { get; }
        public int BestScore();
        void AddScore(int value);
        void SubstractScore(int value);
        public void NewGame();
        void SaveScore();
    }
}