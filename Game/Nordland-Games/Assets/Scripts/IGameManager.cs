namespace NLG
{
    public interface IGameManager
    {
        /// <summary>
        /// Starts the game.
        /// </summary>
        public void StartGame();
        
        /// <summary>
        /// Applies damage on the player.
        /// </summary>
        public void TakeDamage();
        
        /// <summary>
        /// Gives the Player a point (or multiple).
        /// </summary>
        public void ReceivePoint();
        
        /// <summary>
        /// Ends the game with a GameOver screen.
        /// </summary>
        public void GameOver();
        
        /// <summary>
        /// Pauses the game.
        /// </summary>
        public void PauseGame();
        
        /// <summary>
        /// Resumes the game from a paused state.
        /// </summary>
        public void ResumeGame();
        
        /// <summary>
        /// Toggles between Pause- and InGame-States.
        /// </summary>
        public void TogglePause();
        
        /// <summary>
        /// Returns the current state of the game.
        /// </summary>
        /// <returns>(GameStates) The current state of the game</returns>
        public GameStates GetState();
        
        /// <summary>
        /// Returns the current score of the player.
        /// </summary>
        /// <returns>(float) The current score of the player</returns>
        public float GetScore();
    }
}