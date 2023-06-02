using System;
using System.Collections;
using System.Collections.Generic;
using NLG;
using NLG.Game3;
using UnityEngine;

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
