using UnityEngine;

namespace NLG.Game3
{
    /// <summary>
    /// A death barrier for Game3 to avoid getting things stacked everywhere. (When collision fails)
    /// </summary>
    public class DeathBarrier : MonoBehaviour
    {
        [SerializeField] private Game3Manager gameManager;

        private void OnTriggerEnter2D(Collider2D other)
        {
            //(Player in this game are the crystals)
            Debug.Log("Test 1");
            if (other.CompareTag("Player"))
            {
                Debug.Log("Test 2");
                gameManager.TakeDamage();
                gameManager.CrystalSpawned = false;
                Destroy(other.gameObject);
            }
        }
    }
}