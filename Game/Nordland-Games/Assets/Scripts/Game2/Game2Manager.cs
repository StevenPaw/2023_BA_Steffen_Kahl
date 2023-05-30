using System;
using System.Collections;
using System.Collections.Generic;
using NLG;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game2
{
    public class Game2Manager : MonoBehaviour, IGameManager
    {
        [Header("GameState")]
        [SerializeField] private GameStates gameState;
        [Header("SpawnVariables")]
        [SerializeField] private Vector2 lastSpawnPoint;
        [SerializeField] private float spawnRate;
        [SerializeField] private float spawnTimer;
        [SerializeField] private AnimationCurve spawnRateCurve;
        [Header("Prefabs")]
        [SerializeField] private List<GameObject> ghostPrefabs;
        [Header("Lives")]
        [SerializeField] private Image liveIcon1;
        [SerializeField] private Image liveIcon2;
        [SerializeField] private Image liveIcon3;
        [Header("Screens")]
        [SerializeField] private GameObject startScreen;
        [SerializeField] private GameObject gameOverScreen;
        [SerializeField] private TMP_Text gameOverScoreText;
        [SerializeField] private GameObject characterXPInfo;
        [SerializeField] private TMP_Text characterXPInfoText;
        [SerializeField] private TMP_Text highscoreText;
        [SerializeField] private int totalHighscore;
        [Header("ScoreVariables")]
        [SerializeField] private int score;
        [SerializeField] private int lives;
        [SerializeField] private int maxLives;
        
        private Rect window;

        public GameStates GameState => gameState;

        public int Score => score;

        public int Lives => lives;

        private void Start()
        {
            gameOverScreen.SetActive(false);
            startScreen.SetActive(true);
            window = Screen.safeArea;
            window.yMax -= 300;
            window.yMin += 200;
            window.xMax -= 50;
            window.xMin += 50;
            highscoreText.text = "Highscore: " + PlayerPrefs.GetInt("Highscore_Game2", 0);
            liveIcon1.gameObject.SetActive(true);
        }
        
        public void StartGame()
        {
            spawnTimer = 0;
            score = 0;
            lives = maxLives;
            gameState = GameStates.INGAME;
            liveIcon1.color = Color.red;
            liveIcon2.color = Color.red;
            liveIcon3.color = Color.red;
            characterXPInfo.SetActive(false);
        }

        public void Update()
        {
            if(gameState == GameStates.INGAME)
            {
                spawnRate = spawnRateCurve.Evaluate(score);
                spawnTimer += Time.deltaTime;
                if(spawnTimer >= spawnRate)
                {
                    spawnTimer = 0;
                    SpawnGhost();
                }
            }
        }
        
        private void SpawnGhost()
        {
            Vector2 newSpawnPosition = Vector2.zero;
            //Try to find a new spawn position that is not too close to the last one
            int tries = 0;
            //Keep the while loop safe with an emergency exit after 20 tries
            do
            {
                newSpawnPosition = Camera.main.ScreenToWorldPoint(new Vector2(
                    UnityEngine.Random.Range(window.xMin, window.xMax),
                    UnityEngine.Random.Range(window.yMin, window.yMax)));
            } while (Vector2.Distance(newSpawnPosition, lastSpawnPoint) < 2f && tries++ < 20);
            
            GameObject ghost = Instantiate(ghostPrefabs[UnityEngine.Random.Range(0, ghostPrefabs.Count)], newSpawnPosition, Quaternion.identity);
            ghost.transform.position = new Vector3(ghost.transform.position.x, ghost.transform.position.y, -50);
            lastSpawnPoint = ghost.transform.position;
            ghost.GetComponent<Ghost>().Game2Manager = this;
        }

        public void TakeDamage()
        {
            //Only take damage if the game is running
            if(gameState != GameStates.INGAME) return;
            
            lives -= 1;
            if (lives == 2)
            {
                liveIcon1.color = Color.white;
            }
            else if (lives == 1)
            {
                liveIcon2.color = Color.white;
            }
            else if (lives == 0)
            {
                liveIcon3.color = Color.white;
                GameOver();
            }
        }

        public void ReceivePoint()
        {
            score++;
            highscoreText.text = "Gefangene Seelen: " + score;
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
            gameState = GameStates.GAMEOVER;
            gameOverScreen.SetActive(true);
            gameOverScoreText.text = "Score: " + score;
            if (score > PlayerPrefs.GetInt("Highscore_Game2", 0))
            {
                PlayerPrefs.SetInt("Highscore_Game2", score);
                highscoreText.text = "Highscore: " + score;
            }
            else
            {
                highscoreText.text = "Highscore: " + PlayerPrefs.GetInt("Highscore_Game2", 0);
            }
            characterXPInfoText.text = "+ " + (score / 3) + " XP";
            characterXPInfo.SetActive(true);
            
            WebManager.instance.AddXP(score / 3);
            WebManager.instance.AddHighscore(2, score);
        }
    }
}