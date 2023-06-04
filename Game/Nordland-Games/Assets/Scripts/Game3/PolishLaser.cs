using System;
using UnityEngine;

namespace NLG.Game3
{
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