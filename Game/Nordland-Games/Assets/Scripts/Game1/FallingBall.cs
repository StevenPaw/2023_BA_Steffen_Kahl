using UnityEngine;

namespace NLG.Game1
{
    public class FallingBall : MonoBehaviour
    {
        [SerializeField] private bool isFlame;

        private void Update()
        {
            if (transform.position.y < -10)
            {
                Destroy(gameObject);
            }
        }
    }
}