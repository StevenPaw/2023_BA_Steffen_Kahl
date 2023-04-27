using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterRenderer : MonoBehaviour
{
    [SerializeField] private Image imgSkinColor;
    [SerializeField] private Image imgEyes;
    [SerializeField] private Image imgMouth;
    [SerializeField] private Image imgHair;
    [SerializeField] private Image imgBottom;
    [SerializeField] private Image imgTop;
    [SerializeField] private Image imgHat;
    
    private WebManager webManager;

    private void Start()
    {
        webManager = WebManager.instance;

        if (webManager.SelectedSkinColor != 0)
        {
            imgSkinColor.sprite = webManager.CharacterParts[webManager.SelectedSkinColor].Image;
        }

        if (webManager.SelectedEyes != 0)
        {
            imgEyes.sprite = webManager.CharacterParts[webManager.SelectedEyes].Image;
        }

        if(webManager.SelectedMouth != 0)
        {
            imgMouth.sprite = webManager.CharacterParts[webManager.SelectedMouth].Image;
        }
        
        if(webManager.SelectedHair != 0)
        {
            imgHair.sprite = webManager.CharacterParts[webManager.SelectedHair].Image;
        }
        
        if(webManager.SelectedBottom != 0)
        {
            imgBottom.sprite = webManager.CharacterParts[webManager.SelectedBottom].Image;
        }
        
        if(webManager.SelectedTop != 0)
        {
            imgTop.sprite = webManager.CharacterParts[webManager.SelectedTop].Image;
        }
        
        if(webManager.SelectedHat != 0)
        {
            imgHat.sprite = webManager.CharacterParts[webManager.SelectedHat].Image;
        }
    }
}
