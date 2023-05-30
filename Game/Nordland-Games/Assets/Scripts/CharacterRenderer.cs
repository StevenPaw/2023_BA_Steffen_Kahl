using System;
using System.Collections;
using System.Collections.Generic;
using NLG;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    [SerializeField] private Image imgBackDeco;
    
    [SerializeField] private int selectedSkinColor;
    [SerializeField] private int selectedEyes;
    [SerializeField] private int selectedMouth;
    [SerializeField] private int selectedHair;
    [SerializeField] private int selectedBottom;
    [SerializeField] private int selectedTop;
    [SerializeField] private int selectedHat;
    [SerializeField] private int selectedBackDeco;
    [SerializeField] private string sceneToLoadAfterSaving;
    
    private WebManager webManager;
    
    public int SelectedBottom => selectedBottom;
    public int SelectedTop => selectedTop;
    public int SelectedHat => selectedHat;
    public int SelectedSkinColor => selectedSkinColor;
    public int SelectedEyes => selectedEyes;
    public int SelectedMouth => selectedMouth;
    public int SelectedHair => selectedHair;
    public int SelectedBackDeco => selectedBackDeco;

    private void Start()
    {
        webManager = WebManager.instance;

        if (webManager)
        {
            if (webManager.SelectedSkinColor != 0)
            {
                imgSkinColor.sprite = webManager.GetCharacterPartByID(webManager.SelectedSkinColor).Image;
                selectedSkinColor = webManager.SelectedSkinColor;
            }

            if (webManager.SelectedEyes != 0)
            {
                imgEyes.sprite = webManager.GetCharacterPartByID(webManager.SelectedEyes).Image;
                selectedEyes = webManager.SelectedEyes;
            }

            if (webManager.SelectedMouth != 0)
            {
                imgMouth.sprite = webManager.GetCharacterPartByID(webManager.SelectedMouth).Image;
                selectedMouth = webManager.SelectedMouth;
            }

            if (webManager.SelectedHair != 0)
            {
                imgHair.sprite = webManager.GetCharacterPartByID(webManager.SelectedHair).Image;
                selectedHair = webManager.SelectedHair;
            }

            if (webManager.SelectedBottom != 0)
            {
                imgBottom.sprite = webManager.GetCharacterPartByID(webManager.SelectedBottom).Image;
                selectedBottom = webManager.SelectedBottom;
            }

            if (webManager.SelectedTop != 0)
            {
                imgTop.sprite = webManager.GetCharacterPartByID(webManager.SelectedTop).Image;
                selectedTop = webManager.SelectedTop;
            }

            if (webManager.SelectedHat != 0)
            {
                imgHat.sprite = webManager.GetCharacterPartByID(webManager.SelectedHat).Image;
                selectedHat = webManager.SelectedHat;
            }

            if (webManager.SelectedBackDeco != 0)
            {
                imgBackDeco.sprite = webManager.GetCharacterPartByID(webManager.SelectedBackDeco).Image;
                selectedBackDeco = webManager.SelectedBackDeco;
            }
        }
    }

    public void ChangeSelectedCharacterPart(CharacterPartTypes type, int changedID)
    {
        switch (type)
        {
            case CharacterPartTypes.SKINCOLOR:
                selectedSkinColor = changedID;
                imgSkinColor.sprite = webManager.GetCharacterPartByID(selectedSkinColor).Image;
                break;
            case CharacterPartTypes.EYES:
                selectedEyes = changedID;
                imgEyes.sprite = webManager.GetCharacterPartByID(selectedEyes).Image;
                break;
            case CharacterPartTypes.MOUTH:
                selectedMouth = changedID;
                imgMouth.sprite = webManager.GetCharacterPartByID(selectedMouth).Image;
                break;
            case CharacterPartTypes.HAIR:
                selectedHair = changedID;
                imgHair.sprite = webManager.GetCharacterPartByID(selectedHair).Image;
                break;
            case CharacterPartTypes.BOTTOM:
                selectedBottom = changedID;
                imgBottom.sprite = webManager.GetCharacterPartByID(selectedBottom).Image;
                break;
            case CharacterPartTypes.TOP:
                selectedTop = changedID;
                imgTop.sprite = webManager.GetCharacterPartByID(selectedTop).Image;
                break;
            case CharacterPartTypes.HAT:
                selectedHat = changedID;
                imgHat.sprite = webManager.GetCharacterPartByID(selectedHat).Image;
                break;
            case CharacterPartTypes.BACKDECO:
                selectedBackDeco = changedID;
                imgBackDeco.sprite = webManager.GetCharacterPartByID(selectedBackDeco).Image;
                break;
        }
    }

    public void SaveChangedCharacter()
    {
        webManager.SetNewBodyParts(selectedSkinColor, selectedEyes, selectedMouth, selectedHair, selectedBottom, selectedTop, selectedHat, selectedBackDeco);
        SceneManager.LoadScene(sceneToLoadAfterSaving);
    }
}
