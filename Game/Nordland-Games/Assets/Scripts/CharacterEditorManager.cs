using System;
using System.Collections;
using System.Collections.Generic;
using NLG;
using UnityEngine;

public class CharacterEditorManager : MonoBehaviour
{
    [SerializeField] private List<CharacterPart> parts;
    [SerializeField] private CharacterPartTypes activePartType;
    [SerializeField] private GameObject selectorEntryPrefab;
    [SerializeField] private GameObject selectorEntryContainer;

    public CharacterPartTypes ActivePartType => activePartType;

    // Start is called before the first frame update
    void Start()
    {
        parts = WebManager.instance.CharacterParts;

        foreach (Transform childTransforms in selectorEntryContainer.transform)
        {
            Destroy(childTransforms.gameObject);
        }

        foreach (var part in parts)
        {
            if (part.Type == activePartType)
            {
                GameObject newObject = Instantiate(selectorEntryPrefab.transform, selectorEntryContainer.transform).gameObject;
                CharacterPartSelector newSelector = newObject.GetComponent<CharacterPartSelector>();
                newSelector.Type = part.Type;
                newSelector.PartID = part.PartID;
                newSelector.Image = part.Image;
            }
        }
    }

    public void ChangeActiveCategory(string newType)
    {
        activePartType = Enum.Parse<CharacterPartTypes>(newType);
        
        foreach (Transform childTransforms in selectorEntryContainer.transform)
        {
            Destroy(childTransforms.gameObject);
        }

        foreach (var part in parts)
        {
            if (part.Type == activePartType)
            {
                GameObject newObject = Instantiate(selectorEntryPrefab.transform, selectorEntryContainer.transform).gameObject;
                CharacterPartSelector newSelector = newObject.GetComponent<CharacterPartSelector>();
                newSelector.Type = part.Type;
                newSelector.PartID = part.PartID;
                newSelector.Image = part.Image;
            }
        }
    }
}
