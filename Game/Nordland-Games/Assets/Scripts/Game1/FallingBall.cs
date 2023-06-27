using UnityEngine;

namespace NLG.Game1
{
    /// <summary>
    /// A simple script that destroys the object when it falls below a certain point.
    /// </summary>
    public class FallingBall : MonoBehaviour
    {
        private void Update()
        {
            if (transform.position.y < -10)
            {
                //If below -10, destroy the object.
                Destroy(gameObject);
            }
        }
    }
}