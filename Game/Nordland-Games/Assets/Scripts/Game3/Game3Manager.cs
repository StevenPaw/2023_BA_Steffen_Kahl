using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NLG.Game3
{
    public class Game3Manager : MonoBehaviour, IGameManager
    {
        [Header("GameState")]
        [SerializeField] private GameStates gameState;
        
        [Header("Prefabs")]
        [SerializeField] private GameObject ballPrefab;
        
        [Header("SpawnVariables")]
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Vector2 maxSpawnDirection;
        [SerializeField] private Vector2 minSpawnDirection;
        
        [Header("ScoreVariables")]
        [SerializeField] private int score;
        [SerializeField] private int lives;
        [SerializeField] private int maxLives;
        [SerializeField] private float shineTimer = 0;
        
        [Header("UI Elements")]
        [SerializeField] private TMP_Text highscoreText;
        [SerializeField] private int totalHighscore;
        [SerializeField] private TMP_Text gameOverScoreText;
        [SerializeField] private GameObject gameOverScreen;
        [SerializeField] private GameObject characterXPInfo;
        [SerializeField] private TMP_Text characterXPInfoText;

        [SerializeField] private Image liveIcon1;
        [SerializeField] private Image liveIcon2;
        [SerializeField] private Image liveIcon3;

        [Header("Other")] 
        [SerializeField] private bool crystalSpawned;

        private Camera mainCamera;
        private Rect window;

        public bool CrystalSpawned
        {
            get => crystalSpawned;
            set => crystalSpawned = value;
        }

        private void Start()
        {
            window = Screen.safeArea;
            mainCamera = Camera.main;
            highscoreText.text = "Highscore: " + PlayerPrefs.GetInt("Highscore_Game3", 0);
            characterXPInfo.SetActive(false);
        }

        private void Update()
        {
            if(gameState == GameStates.INGAME && !crystalSpawned)
            {
                SpawnObject();
            }
        }
        
        private void SpawnObject()
        {
            float randomX = Random.Range(minSpawnDirection.x, maxSpawnDirection.x);
            float randomY = Random.Range(minSpawnDirection.y, maxSpawnDirection.y);
            Vector2 randomDirection = new Vector2(randomX, randomY);
            Debug.Log(randomDirection);
            GameObject ball = Instantiate(ballPrefab, spawnPoint.position, Quaternion.identity);
            ball.GetComponent<Rigidbody2D>().AddForce(randomDirection);
            crystalSpawned = true;
        }

        public void StartGame()
        {
            score = 0;
            lives = maxLives;
            gameState = GameStates.INGAME;
            liveIcon1.color = Color.red;
            liveIcon2.color = Color.red;
            liveIcon3.color = Color.red;
            characterXPInfo.SetActive(false);
        }

        public void TakeDamage()
        {
            lives -= 1;

            if (lives <= 2)
            {
                liveIcon1.color = Color.grey;
                liveIcon2.color = Color.red;
                liveIcon3.color = Color.red;
            }
            
            if (lives <= 1)
            {
                liveIcon1.color = Color.grey;
                liveIcon2.color = Color.grey;
                liveIcon3.color = Color.red;
            }
            
            if (lives <= 0)
            {
                GameOver();
            }
        }

        public void ReceivePoint()
        {
            score += 1;
        }

        public void PauseGame()
        {
            gameState = GameStates.PAUSED;
            Time.timeScale = 0;
        }
        
        public void ResumeGame()
        {
            gameState = GameStates.INGAME;
            Time.timeScale = 1;
        }
        
        public void TogglePause()
        {
            if (gameState == GameStates.PAUSED)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        public void GameOver()
        {
            liveIcon1.color = Color.grey;
            liveIcon2.color = Color.grey;
            liveIcon3.color = Color.grey;
            gameState = GameStates.GAMEOVER;
            gameOverScreen.SetActive(true);
            gameOverScoreText.text = "Deine Punktzahl: \n" + score + " Schneebälle";
                
            if(score > PlayerPrefs.GetInt("Highscore_Game3", 0))
            {
                PlayerPrefs.SetInt("Highscore_Game1", score);
            }
            totalHighscore = PlayerPrefs.GetInt("Highscore_Game3", 0);
            highscoreText.text = "Highscore: " + totalHighscore;
            characterXPInfo.SetActive(true);
            characterXPInfoText.text = "+ " + score / 3 + " XP";
            if (WebManager.instance != null)
            {
                WebManager.instance.AddXP(score / 3);
                WebManager.instance.AddHighscore(1, score);
            }
        }
    }
}