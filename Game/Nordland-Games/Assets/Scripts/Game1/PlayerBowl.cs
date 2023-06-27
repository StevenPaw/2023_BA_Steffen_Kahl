using System.Linq;
using UnityEngine;

namespace NLG.Game1
{
    /// <summary>
    /// A controller for the bowl, the player is controlling, to give points and take damage.
    /// </summary>
    public class PlayerBowl : MonoBehaviour
    {
        private IGameManager gameManager;

        private void Start()
        {
            //Get the current game controller. In this case preferably the Game1Manager.
            gameManager = FindObjectsOfType<MonoBehaviour>().OfType<IGameManager>().FirstOrDefault();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Fireball"))
            {
                gameManager.TakeDamage();
                Destroy(col.gameObject);
            }
            else if (col.CompareTag("Snowball"))
            {
                gameManager.ReceivePoint();
                Destroy(col.gameObject);
            }
        }
    }
}