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
        [SerializeField] private float score;
        [SerializeField] private int lives;
        [SerializeField] private int maxLives;
        
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

        public GameStates GameState => gameState;

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

            if (gameState == GameStates.INGAME)
            {
                highscoreText.text = "Polierte Materie: " + score.ToString("F0");
            }
            
        }
        
        private void SpawnObject()
        {
            float randomX = Random.Range(minSpawnDirection.x, maxSpawnDirection.x);
            float randomY = Random.Range(minSpawnDirection.y, maxSpawnDirection.y);
            Vector2 randomDirection = new Vector2(randomX, randomY);
            GameObject ball = Instantiate(ballPrefab, spawnPoint.position, Quaternion.identity);
            ball.GetComponent<Rigidbody2D>().AddForce(randomDirection);
            crystalSpawned = true;
        }

        public void StartGame()
        {
            score = 0;
            lives = maxLives;
            gameState = GameStates.INGAME;
            liveIcon1.color = Color.cyan;
            liveIcon2.color = Color.cyan;
            liveIcon3.color = Color.cyan;
            characterXPInfo.SetActive(false);
        }

        public void TakeDamage()
        {
            lives -= 1;

            if (lives <= 2)
            {
                liveIcon1.color = Color.grey;
                liveIcon2.color = Color.cyan;
                liveIcon3.color = Color.cyan;
            }
            
            if (lives <= 1)
            {
                liveIcon1.color = Color.grey;
                liveIcon2.color = Color.grey;
                liveIcon3.color = Color.cyan;
            }
            
            if (lives <= 0)
            {
                liveIcon1.color = Color.grey;
                liveIcon2.color = Color.grey;
                liveIcon3.color = Color.grey;
                GameOver();
            }
        }

        public void ReceivePoint()
        {
            score += 0.1f;
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
            gameOverScoreText.text = "Deine Punktzahl: \n" + (int)score + " polierte Materie";
                
            if(score > PlayerPrefs.GetInt("Highscore_Game3", 0))
            {
                PlayerPrefs.SetInt("Highscore_Game3", (int)score);
            }
            totalHighscore = PlayerPrefs.GetInt("Highscore_Game3", 0);
            highscoreText.text = "Highscore: " + totalHighscore;
            characterXPInfo.SetActive(true);
            characterXPInfoText.text = "+ " + (int)score / 5 + " XP";
            if (WebManager.instance != null)
            {
                WebManager.instance.AddXP((int)score / 5);
                WebManager.instance.AddHighscore(3, (int)score);
            }
        }
    }
}