using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NLG.Game1
{
    public class Game1Manager : MonoBehaviour
    {
        [Header("GameState")]
        [SerializeField] private GameStates gameState;
        [Header("Prefabs")]
        [SerializeField] private GameObject fireBallPrefab;
        [SerializeField] private GameObject snowballPrefab;
        [Header("SpawnVariables")]
        [SerializeField] private Vector2 lastSpawnPoint;
        [SerializeField] private float spawnRate;
        [SerializeField] private float spawnTimer;
        [SerializeField] private AnimationCurve spawnRateCurve;
        [Header("ScoreVariables")]
        [SerializeField] private int score;
        [SerializeField] private int lives;
        [SerializeField] private int maxLives;
        [SerializeField] private GameObject playerBowl;
        [SerializeField] private Vector2 playerBowlStartPosition;
        [SerializeField] private Vector2 playerBowlTarget;
        [SerializeField] private float playerBowlSpeed;
        [SerializeField] private AnimationCurve playerBowlSpeedCurve;
        
        [SerializeField] private TMP_Text highscoreText;
        [SerializeField] private int totalHighscore;
        [SerializeField] private TMP_Text gameOverScoreText;
        [SerializeField] private GameObject gameOverScreen;

        [SerializeField] private Image liveIcon1;
        [SerializeField] private Image liveIcon2;
        [SerializeField] private Image liveIcon3;

        [SerializeField] private GameObject characterXPInfo;
        [SerializeField] private TMP_Text characterXPInfoText;
        
        private Rect window;

        private void Start()
        {
            window = Screen.safeArea;
            playerBowlStartPosition = Camera.main.ScreenToWorldPoint(new Vector2(window.width / 2, 100));
            playerBowl.gameObject.SetActive(false);
            highscoreText.text = "Highscore: " + PlayerPrefs.GetInt("Highscore_Game1", 0);
            characterXPInfo.SetActive(false);
        }

        private void Update()
        {
            if(gameState == GameStates.INGAME)
            {
                spawnTimer += Time.deltaTime;
                
                //Move Player
                if (Vector2.Distance(playerBowl.transform.position, playerBowlTarget) > 0.01)
                {
                    Vector3 newPlayerPos = Vector2.MoveTowards(playerBowl.transform.position, playerBowlTarget,
                        playerBowlSpeed * Time.deltaTime);
                    newPlayerPos.z = -5;
                    playerBowl.transform.position = newPlayerPos;
                }
                
                if(Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);
                    playerBowlTarget = Camera.main.ScreenToWorldPoint(touch.position);
                } 
                
                //Spawn Objects
                if(spawnTimer >= spawnRate)
                {
                    spawnTimer = 0;
                    SpawnObject();
                }
                
                //Increase Speed
                spawnRate = spawnRateCurve.Evaluate(score);
                playerBowlSpeed = playerBowlSpeedCurve.Evaluate(score);
                Debug.Log(spawnRate);
                highscoreText.text = "Schneebälle: " + score;
            }
        }
        
        private void SpawnObject()
        {
            GameObject prefab = null;
            int random = UnityEngine.Random.Range(0, 4);
            if(random == 0)
            {
                prefab = fireBallPrefab;
            }
            else
            {
                prefab = snowballPrefab;
            }

            Vector3 newPos = new Vector3(0, 0, 0);
            int emergencyBreaker = 0;

            //Calculate new Positions until it is far away enough from the last spawn point
            do
            {
                newPos = Camera.main.ScreenToWorldPoint(new Vector3(UnityEngine.Random.Range(0, window.width),
                    window.height + (window.height * 0.5f), 20));

                //Break the while loop if it takes too long
                emergencyBreaker += 1;
                if (emergencyBreaker > 100)
                {
                    break;
                }
            } while (Vector3.Distance(lastSpawnPoint, newPos) <= 1);
            lastSpawnPoint = newPos;

            GameObject obj = Instantiate(prefab, newPos, Quaternion.identity);
            obj.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -5);
        }

        public void StartGame()
        {
            spawnTimer = 0;
            score = 0;
            lives = maxLives;
            playerBowl.gameObject.SetActive(true);
            playerBowl.transform.position = playerBowlStartPosition;
            playerBowlTarget = playerBowlStartPosition;
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
                liveIcon1.color = Color.grey;
                liveIcon2.color = Color.grey;
                liveIcon3.color = Color.grey;
                gameState = GameStates.GAMEOVER;
                gameOverScreen.SetActive(true);
                playerBowlTarget = playerBowlStartPosition;
                playerBowl.transform.position = playerBowlStartPosition;
                playerBowl.gameObject.SetActive(false);
                gameOverScoreText.text = "Deine Punktzahl: \n" + score + " Schneebälle";
                
                if(score > PlayerPrefs.GetInt("Highscore_Game1", 0))
                {
                    PlayerPrefs.SetInt("Highscore_Game1", score);
                }
                totalHighscore = PlayerPrefs.GetInt("Highscore_Game1", 0);
                highscoreText.text = "Highscore: " + totalHighscore;
                characterXPInfo.SetActive(true);
                characterXPInfoText.text = "+ " + score / 10 + " XP";
            }
        }

        public void ReceivePoint()
        {
            score += 1;
        }
    }
}