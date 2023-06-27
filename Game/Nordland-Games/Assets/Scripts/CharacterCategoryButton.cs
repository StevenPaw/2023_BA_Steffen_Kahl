using UnityEngine;
using UnityEngine.UI;

namespace NLG
{
    /// <summary>
    /// A script for all CategoryButtons in the Character Creator.
    /// </summary>
    public class CharacterCategoryButton : MonoBehaviour
    {
        [SerializeField] private CharacterPartTypes type;
        [SerializeField] private Color inactiveColor;
        [SerializeField] private Color activeColor;
        [SerializeField] private CharacterEditorManager characterEditorManager;
        [SerializeField] private Image image;

        private void Update()
        {
            if (characterEditorManager.ActivePartType == type)
            {
                image.color = activeColor;
            }
            else
            {
                image.color = inactiveColor;
            }
        }
    }
}