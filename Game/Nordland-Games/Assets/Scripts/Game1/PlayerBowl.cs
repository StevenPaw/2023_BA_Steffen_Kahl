using UnityEngine;

namespace NLG.Game1
{
    public class PlayerBowl : MonoBehaviour
    {
        [SerializeField] private Game1Manager game1Manager;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Fireball"))
            {
                TakeDamage();
                Destroy(col.gameObject);
            }
            else if (col.CompareTag("Snowball"))
            {
                ReceivePoint();
                Destroy(col.gameObject);
            }
        }

        private void TakeDamage()
        {
            game1Manager.TakeDamage();
        }

        private void ReceivePoint()
        {
            game1Manager.ReceivePoint();
        }
    }
}