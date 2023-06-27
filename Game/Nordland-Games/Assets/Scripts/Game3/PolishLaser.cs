using System;
using UnityEngine;

namespace NLG.Game3
{
    /// <summary>
    /// A script on the laser in Game3 that adds points to the player when they are hit.
    /// </summary>
    public class PolishLaser : MonoBehaviour
    {
        private Game3Manager gameManager;

        private void Start()
        {
            gameManager = FindObjectOfType<Game3Manager>();
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (gameManager.GameState != GameStates.INGAME) return;
            if (other.CompareTag("Player"))
            {
                other.GetComponent<CrystalGrade>().AddPoints();
                gameManager.ReceivePoint();
            }
        }
    }
}