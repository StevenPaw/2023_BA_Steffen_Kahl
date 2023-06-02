using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PumpClickable : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Vector2 forceOnClick;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        rb.AddForce(forceOnClick, ForceMode2D.Impulse);
        Debug.Log("Clicked");
    }
}
