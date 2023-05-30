namespace NLG
{
    public interface IGameManager
    {
        public void StartGame();
        public void TakeDamage();
        public void ReceivePoint();
        public void GameOver();
        public void PauseGame();
        public void ResumeGame();
        public void TogglePause();
    }
}