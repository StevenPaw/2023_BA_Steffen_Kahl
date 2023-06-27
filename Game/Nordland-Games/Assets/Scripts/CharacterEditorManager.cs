using System;
using System.Collections.Generic;
using UnityEngine;

namespace NLG
{
    /// <summary>
    /// A manager that controls the Character Creator.
    /// </summary>
    public class CharacterEditorManager : MonoBehaviour
    {
        [SerializeField] private List<CharacterPart> parts;
        [SerializeField] private CharacterPartTypes activePartType;
        [SerializeField] private GameObject selectorEntryPrefab;
        [SerializeField] private GameObject selectorEntryContainer;
        [SerializeField] private GameObject[] categoryButtons;

        public CharacterPartTypes ActivePartType => activePartType;

        // Start is called before the first frame update
        void Start()
        {
            foreach (GameObject button in categoryButtons)
            {
                button.GetComponent<RectTransform>()
                    .SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.width / 15.5f);
            }

            parts = WebManager.instance.CharacterParts;

            foreach (Transform childTransforms in selectorEntryContainer.transform)
            {
                Destroy(childTransforms.gameObject);
            }

            foreach (var part in parts)
            {
                if (part.Type == activePartType)
                {
                    GameObject newObject = Instantiate(selectorEntryPrefab.transform, selectorEntryContainer.transform)
                        .gameObject;
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
                    GameObject newObject = Instantiate(selectorEntryPrefab.transform, selectorEntryContainer.transform)
                        .gameObject;
                    CharacterPartSelector newSelector = newObject.GetComponent<CharacterPartSelector>();
                    newSelector.Type = part.Type;
                    newSelector.PartID = part.PartID;
                    newSelector.Image = part.Image;
                }
            }
        }
    }
}