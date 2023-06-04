using UnityEngine;

namespace NLG.Game3
{
    public class DeathBarrier : MonoBehaviour
    {
        [SerializeField] private Game3Manager gameManager;

        private void OnTriggerEnter2D(Collider2D other)
        {
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