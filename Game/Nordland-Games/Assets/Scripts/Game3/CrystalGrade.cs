using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class CrystalGrade : MonoBehaviour
{
    [SerializeField] private float grade;
    [SerializeField] private Sprite level0;
    [SerializeField] private Sprite level1;
    [SerializeField] private Sprite level2;
    [SerializeField] private Sprite level3;
    [SerializeField] private Sprite level4;
    [SerializeField] private Sprite level5;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Light2D light;
    [SerializeField] private Color[] colors;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        light = GetComponentInChildren<Light2D>();
        
        spriteRenderer.sprite = level0;
        Color newColor = colors[Random.Range(0, colors.Length)];
        light.color = newColor;
        spriteRenderer.color = newColor;
    }

    public void AddPoints()
    {
        grade += 0.1f;

        if (grade < 3f)
        {
            spriteRenderer.sprite = level0;
        } else if (grade < 10f)
        {
            spriteRenderer.sprite = level1;
        } else if (grade < 25f)
        {
            spriteRenderer.sprite = level2;
        } else if (grade < 40f)
        {
            spriteRenderer.sprite = level3;
        } else if (grade < 60f)
        {
            spriteRenderer.sprite = level4;
        } else if (grade < 100f)
        {
            spriteRenderer.sprite = level5;
        }
    }
}
